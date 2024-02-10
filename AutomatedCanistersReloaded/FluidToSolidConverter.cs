namespace AutomatedCanistersReloaded
{
	public class FluidToSolidConverter : StateMachineComponent<FluidToSolidConverter.StatesInstance>
	{
		[MyCmpAdd]
		private SolidConduitDispenser solidConduitDispenser;

		protected override void OnSpawn()
		{
			base.OnSpawn();
			smi.StartSM();
		}

		public class States : GameStateMachine<States, StatesInstance, FluidToSolidConverter>
		{
			public State off;
			public ReadyStates on;

			public override void InitializeStates(out BaseState default_state)
			{
				default_state = off;
				off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, on, smi => smi.GetComponent<Operational>().IsOperational).Enter(smi => smi.GetComponent<Operational>().SetActive(false));
				on.idle.PlayAnim("on").Transition(on.working, smi => smi.master.solidConduitDispenser.IsDispensing);

				if (Options.Instance.AlwaysActive)
				{
					on.DefaultState(on.idle).EventTransition(GameHashes.OperationalChanged, off, smi => !smi.GetComponent<Operational>().IsOperational).Enter(smi => smi.GetComponent<Operational>().SetActive(true));
					on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(on.idle, smi => !smi.master.solidConduitDispenser.IsDispensing);
				}
				else
				{
					on.DefaultState(on.idle).EventTransition(GameHashes.OperationalChanged, off, smi => !smi.GetComponent<Operational>().IsOperational);
					on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(on.idle, smi => !smi.master.solidConduitDispenser.IsDispensing).Enter(smi => smi.GetComponent<Operational>().SetActive(true)).Exit(smi => smi.GetComponent<Operational>().SetActive(false));
				}
			}

			public class ReadyStates : State
			{
				public State idle;
				public State working;
			}
		}

		public class StatesInstance : GameStateMachine<States, StatesInstance, FluidToSolidConverter, object>.GameInstance
		{
			public StatesInstance(FluidToSolidConverter smi) : base(smi)
			{
			}
		}
	}

}
