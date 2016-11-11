using UnityEngine;
using SimpleContainer.Container;

namespace SimpleContainer.Examples.Commander
{
	public class RotateGameObjectCommand : Command, IUpdatable
    {
		protected Transform objectToRotate;
		
		public override void Execute(params object[] parameters) {
			objectToRotate = (Transform)parameters[0];

            // ���� Retain() ���������� command �� Execute() ����ִ�к��������
            // ��ʹ����Խ��� Update �¼���command �������ͷţ������ͷſɵ��� Release() ����
            Retain();
		}

		public void Update () {
			objectToRotate.Rotate(1.0f, 1.0f, 1.0f);
		}
	}
}