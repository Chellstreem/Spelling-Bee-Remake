using System.Collections.Generic;
using Units;

namespace VFX
{
    public class UnitStatusController
    {
        private readonly ComplexUnit unit;
        private readonly CoroutineRunner coroutineRunner;
        private readonly Dictionary<UnitStatusType, UnitStatus> _statusMap = new();

        public UnitStatus CurrentStatus { get; private set; }

        public UnitStatusController(ComplexUnit unit, CoroutineRunner coroutineRunner)
        {
            this.unit = unit;
            this.coroutineRunner = coroutineRunner;
            InitializeStatusMap(unit.Statuses);
        }

        public void SetStatus(UnitStatusType status, float duration = 0f)
        {
            CurrentStatus?.Exit();
            CurrentStatus = _statusMap[status];
            CurrentStatus.Duration = duration;
            CurrentStatus.Enter();
        }

        private void InitializeStatusMap(UnitStatusDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                if (_statusMap.ContainsKey(definition.Type)) return;

                UnitStatus status = definition.CreateStatus(unit, coroutineRunner);
                _statusMap[definition.Type] = status;
            }
        }
    }
}