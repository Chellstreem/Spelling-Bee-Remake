using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Threading;

namespace Movement
{
    public class ObjectMover
    {
        private CancellationTokenSource _moveCancellation;

        public async UniTask MoveTo(Transform objTransform, Vector3 targetPosition, float speed, float tolerance = 0.01f)
        {
            float sqrTolerance = tolerance * tolerance;

            if (IsClose(objTransform.position, targetPosition, sqrTolerance))
                return;

            _moveCancellation?.Cancel();
            _moveCancellation?.Dispose();

            _moveCancellation = new CancellationTokenSource();

            await RunMoveTask(objTransform, targetPosition, speed, sqrTolerance, _moveCancellation.Token);
        }

        private async UniTask RunMoveTask(Transform objTransform, Vector3 targetPosition, float speed,
            float sqrTolerance, CancellationToken cancellationToken)
        {
            while ((objTransform.position - targetPosition).sqrMagnitude > sqrTolerance)
            {
                cancellationToken.ThrowIfCancellationRequested();

                objTransform.position = Vector3.MoveTowards(
                    objTransform.position,
                    targetPosition,
                    speed * Time.deltaTime);

                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            }

            objTransform.position = targetPosition;
        }

        private bool IsClose(Vector3 a, Vector3 b, float sqrTolerance)
        {
            return (a - b).sqrMagnitude < sqrTolerance;
        }
    }
}