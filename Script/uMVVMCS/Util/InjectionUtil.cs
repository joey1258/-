/*
 * Copyright 2016 Sun Ning��Joey1258��
 *
 *	Licensed under the Apache License, Version 2.0 (the "License");
 *	you may not use this file except in compliance with the License.
 *	You may obtain a copy of the License at
 *
 *		http://www.apache.org/licenses/LICENSE-2.0
 *
 *		Unless required by applicable law or agreed to in writing, software
 *		distributed under the License is distributed on an "AS IS" BASIS,
 *		WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *		See the License for the specific language governing permissions and
 *		limitations under the License.
 */

using UnityEngine;
using System;
using uMVVMCS.DIContainer;

namespace uMVVMCS
{
	/// <summary>
	/// ע�빤����
	/// </summary>
	public static class InjectionUtil {
        /// <summary>
        /// Injects into a specified object using container details.
        /// ��ȡ���� obj �ϸ��ŵ� InjectFromContainer�������Ե� id ִ��ע��
        /// </summary>
        public static void Inject(object obj)
        {
            // ��ȡ��������������
			var attributes = obj.GetType().GetCustomAttributes(true);
			
            // ���û�л�ȡ�������ÿ� id ����ע��
			if (attributes.Length == 0) { Inject(obj, null); }
            else
            {
				var containInjectFromContainer = false;
				
				for (var i = 0; i < attributes.Length; i++)
                {
					var attribute = attributes[i];
                    // ����� InjectFromContainer ���ԣ������Ե� id ִ��ע��
                    if (attribute is InjectFromContainer)
                    {
						Inject(obj, (attribute as InjectFromContainer).id);
						containInjectFromContainer = true;
					}
				}

                //��������û�л�ȡ�� InjectFromContainer ���ԣ��ÿ� id ����ע��
                if (!containInjectFromContainer) { Inject(obj, null); }
			}
		}

        /// <summary>
        /// ������� obj ���ǵ��� binding ��ֵ��Ϊ ContextRoot �� containersData List ��ÿ��
        /// Ԫ�ص� container id ����� id ��ȵ�����ע�� obj��
        /// ����� id Ϊ�գ�ֻҪ obj ���ǵ����Ͷ�ÿһ����������ע�룬����ֻ������ id ��ȵ�����ע��
        /// </summary>
        public static void Inject(object obj, object id)
        {
            // ��ȡ ContextRoot �е� containersData List
            var containers = ContextRoot.containersData;

            for (int i = 0; i < containers.Count; i++) {
				var container = containers[i].container;
                // ���� list��������� id ��Ϊ���ҺͲ��� id ��ȣ�injectOnContainer Ϊ��
                var injectOnContainer = (container.id != null && container.id.Equals(id));

                // ����� id Ϊ�ջ� injectOnContainer Ϊ�棬�Ҳ��� obj �ǵ�������Ϊ��ǰ����ע�� obj
                if ((id == null || injectOnContainer) && 
                    !IsSingletonOnContainer(obj, container))
                {
					container.Inject(obj);
				}
			}
		}
		
		/// <summary>
		/// ����ָ�������е� object �Ƿ��ǵ���
		/// </summary>
		public static bool IsSingletonOnContainer(object obj, IInjectionContainer container)
        {
			var isSingleton = false;
			var bindings = container.GetBindingsByType(obj.GetType());

            if (bindings == null) { return false; }
			
			for (var i = 0; i < bindings.Count; i++)
            {
				if (bindings[i].value == obj)
                {
                    isSingleton = true;
                    break;
                }
			}
			
			return isSingleton;
		}
	}
}