using System;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.SpaceShipDataUpdate;
using TMPro;
using UnityEngine;

namespace UI.SpaceShipStatus
{
    [RequireComponent(typeof(TMP_Text))]
    public class AccelerationLabel : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private TMP_Text _label;

        private void Start()
        {
            _label = GetComponent<TMP_Text>();

            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.AccelerationUpdated += UpdateText;
        }

        private void UpdateText(float acceleration) =>
            _label.text =
                $"Acceleration: {Math.Round(acceleration, 2)}";
    }
}