﻿namespace AutomatedCanistersReloaded
{
	public class SolidToLiquidConverter : StateMachineComponent<SolidToLiquidConverter.StatesInstance>
	{
		[MyCmpAdd]
		private Storage storage;
		[MyCmpAdd]
		private SolidConduitConsumer solidConduitConsumer;

		protected override void OnSpawn()
		{
			base.OnSpawn();
			smi.StartSM();
		}

		public class States : GameStateMachine<States, StatesInstance, SolidToLiquidConverter>
		{
			public State off;
			public ReadyStates on;

			public override void InitializeStates(out BaseState default_state)
			{
				default_state = off;
				off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, on, smi => smi.GetComponent<Operational>().IsOperational).Enter(smi => smi.GetComponent<Operational>().SetActive(false));
				on.idle.PlayAnim("on").Transition(on.working, smi => smi.master.solidConduitConsumer.IsConsuming || smi.master.storage.Has(GameTags.Liquid));

				if (Options.Instance.AlwaysActive)
				{
					on.DefaultState(on.idle).EventTransition(GameHashes.OperationalChanged, off, smi => !smi.GetComponent<Operational>().IsOperational).Enter(smi => smi.GetComponent<Operational>().SetActive(true));
					on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(on.idle, smi => !smi.master.solidConduitConsumer.IsConsuming && !smi.master.storage.Has(GameTags.Liquid));
				}
				else
				{
					on.DefaultState(on.idle).EventTransition(GameHashes.OperationalChanged, off, smi => !smi.GetComponent<Operational>().IsOperational);
					on.working.PlayAnim("on_flow", KAnim.PlayMode.Loop).Transition(on.idle, smi => !smi.master.solidConduitConsumer.IsConsuming && !smi.master.storage.Has(GameTags.Liquid)).Enter(smi => smi.GetComponent<Operational>().SetActive(true)).Exit(smi => smi.GetComponent<Operational>().SetActive(false));
				}
			}

			public class ReadyStates : State
			{
				public State idle;
				public State working;
			}
		}

		public class StatesInstance : GameStateMachine<States, StatesInstance, SolidToLiquidConverter, object>.GameInstance
		{
			public StatesInstance(SolidToLiquidConverter smi) : base(smi)
			{
			}
		}
	}

}
