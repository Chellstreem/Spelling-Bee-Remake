using System.Collections.Generic;
using Units;

namespace VFX
{
    public class UnitStatusController
    {
        private readonly InteractableUnit unit;
        private readonly Dictionary<UnitStatusType, UnitStatus> _statusMap = new();

        public UnitStatus CurrentStatus { get; private set; }

        public UnitStatusController(InteractableUnit unit)
        {
            this.unit = unit;
            InitializeStatusMap(unit.Statuses);
        }

        public void SetStatus(UnitStatusType status)
        {
            CurrentStatus?.Exit();
            CurrentStatus = _statusMap[status];
            CurrentStatus.Enter();
        }

        private void InitializeStatusMap(ConstantUnitStatusDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                if (_statusMap.ContainsKey(definition.Type)) return;

                UnitStatus status = definition.CreateStatus(unit);
                _statusMap[definition.Type] = status;
            }
        }
    }
}