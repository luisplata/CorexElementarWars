using System.Collections;
using UnityEngine;

public class LogicOfGame : MonoBehaviour
{
    private StateMachinePattern _stateMachinePattern;
    private IMediatorLogicOfGame _mediatorOfGame;


    public void Configure(IMediatorLogicOfGame mediatorOfGame)
    {
        _mediatorOfGame = mediatorOfGame;
        _stateMachinePattern = new StateMachinePattern();
        _stateMachinePattern.AddState(new ConfigureState(_mediatorOfGame.GetUiHandler(),
            _mediatorOfGame.GetInputHandler()));

        _stateMachinePattern.AddState(new SinchronizeState(_mediatorOfGame.GetUiHandler()));

        _stateMachinePattern.AddState(new GameState(_mediatorOfGame.GetInputHandler(), _mediatorOfGame.GetUiHandler()));

        _stateMachinePattern.Configure();
        StartCoroutine(StateMachine());
    }

    private IEnumerator StateMachine()
    {
        var currentState = _stateMachinePattern.GetState();
        while (currentState is not null)
        {
            currentState.OnEnterState();
            Debug.Assert(currentState != null, nameof(currentState) + " != null");
            while (currentState.IsCompleted())
            {
                yield return null;
            }

            currentState.OnExitState();
            currentState = _stateMachinePattern.GetState(_stateMachinePattern.GetNextState());
        }

        Debug.Log("State Finished");
    }
}