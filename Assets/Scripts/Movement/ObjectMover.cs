using System.Collections;
using UnityEngine;

namespace Movement
{
    public class ObjectMover
    {
        private readonly CoroutineRunner coroutineRunner;
        private Coroutine _moveCoroutine;

        public ObjectMover(CoroutineRunner coroutineRunner)
        {
            this.coroutineRunner = coroutineRunner;
        }

        public void MoveTo(Transform objectTransform, Vector3 targetPosition, float speed, float tolerance = 0.01f)
        {
            float sqrTolerance = tolerance * tolerance;

            if (IsClose(objectTransform.position, targetPosition, sqrTolerance))
                return;

            if (_moveCoroutine != null)
                coroutineRunner.Stop(_moveCoroutine);

            _moveCoroutine = coroutineRunner.Run(RunMoveCoroutine(objectTransform, targetPosition, speed, sqrTolerance));
        }

        private IEnumerator RunMoveCoroutine(Transform objectTransform, Vector3 targetPosition, float speed, float sqrTolerance)
        {
            while ((objectTransform.position - targetPosition).sqrMagnitude > sqrTolerance)
            {
                objectTransform.position = Vector3.MoveTowards(objectTransform.position, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            objectTransform.position = targetPosition;
        }

        private bool IsClose(Vector3 a, Vector3 b, float sqrTolerance) => (a - b).sqrMagnitude < sqrTolerance;
    }
}
