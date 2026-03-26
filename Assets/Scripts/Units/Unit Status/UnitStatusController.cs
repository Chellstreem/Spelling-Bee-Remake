using System.Collections.Generic;
using Units;
using UnityEngine;

namespace VFX
{
    public class UnitStatusController
    {
        private readonly Dictionary<UnitStatusType, UnitStatus> _statusMap = new();
        public UnitStatus CurrentStatus { get; private set; }

        public UnitStatusController(GameConfig config)
        {
            InitializeStatusMap(config.UnitStatuses);
        }

        private void InitializeStatusMap(UnitStatusDefinition[] definitions)
        {
            foreach (var definition in definitions)
            {
                if (_statusMap.ContainsKey(definition.Type)) return;

                UnitStatus status = definition.CreateStatus();
                _statusMap[definition.Type] = status;
            }
        }
    }
}