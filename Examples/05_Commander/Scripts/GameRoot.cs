using UnityEngine;
using uMVVMCS.DIContainer;

namespace uMVVMCS.Examples.Commander
{
	public class GameRoot : ContextRoot
    {
		protected ICommandDispatcher dispatcher;

		public override void SetupContainers()
        {
			// �������
			var container = AddContainer<InjectionContainer>();

			container
				// ע�������������չ
				.RegisterAOT<CommanderContainer>()
				.RegisterAOT<EventContainer>()
				.RegisterAOT<UnityContainer>()
                // ע�� "uMVVMCS.Examples.Commander" �����ռ��µ����� Command
                .RegisterCommands("uMVVMCS.Examples.Commander")
				// �� prefab
				.Bind<Transform>().ToPrefab("05_Commander/Prism");
		
			// ��ȡ commandDispatcher �Ա��� Init() �����е���
			dispatcher = container.GetCommandDispatcher();
		}
		
		public override void Init()
        {
			dispatcher.Dispatch<SpawnGameObjectCommand>();
		}
	}
}