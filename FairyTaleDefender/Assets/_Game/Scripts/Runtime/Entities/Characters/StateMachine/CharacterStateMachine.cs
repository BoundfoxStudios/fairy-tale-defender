using System;
using System.Collections.Generic;
using BoundfoxStudios.FairyTaleDefender.Entities.Characters.ScriptableObjects;
using UnityEngine;

namespace BoundfoxStudios.FairyTaleDefender.Entities.Characters.StateMachine
{
	public class CharacterStateMachine<TDefinition>
		where TDefinition : CharacterSO
	{
		private readonly Character<TDefinition> _character;
		private readonly Dictionary<Type, IState<TDefinition>> _states = new();
		private IState<TDefinition>? _currentState;

		public CharacterStateMachine(Character<TDefinition> character)
		{
			_character = character;
		}

		public void AddState<T>()
			where T : IState<TDefinition>, new()
		{
			var stateType = typeof(T);
			_states.Add(stateType, new T());

			_states[stateType].Initialize(_character, this);
		}

		public void ChangeState<T>()
		{
			if (_currentState is T)
			{
				return;
			}

			Debug.Log($"Exiting state {_currentState?.GetType()}", _character);
			_currentState?.Exit();

			if (!_states.TryGetValue(typeof(T), out var state))
			{
				throw new($"Trying to enter state {typeof(T).Name}, but it was no added.");
			}

			_currentState = state;

			Debug.Log($"Entering state {_currentState.GetType()}", _character);
			_currentState.Enter();
		}
	}
}
