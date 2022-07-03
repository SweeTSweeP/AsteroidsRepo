using System;
using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.SpaceShipDataUpdate;
using TMPro;
using UnityEngine;

namespace UI.SpaceShipStatus
{
    [RequireComponent(typeof(TMP_Text))]
    public class CoordinatesLabel : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private TMP_Text _label;

        private void Start()
        {
            _label = GetComponent<TMP_Text>();

            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.CoordinatesUpdated += UpdateText;
        }

        private void UpdateText(Vector3 coords) =>
            _label.text =
                $"Coordinates: x({Math.Round(coords.x, 2)}) y({Math.Round(coords.y, 2)}) z({Math.Round(coords.z, 2)})";
    }
}