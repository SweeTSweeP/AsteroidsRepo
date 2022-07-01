using System;
using Infrastructure.Services;
using Infrastructure.Services.SpaceShipDataUpdate;
using TMPro;
using UnityEngine;

namespace UI.SpaceShipStatus
{
    [RequireComponent(typeof(TMP_Text))]
    public class AngleLabel : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private TMP_Text _label;

        private void Start()
        {
            _label = GetComponent<TMP_Text>();

            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.AngleUpdated += UpdateText;
        }

        private void UpdateText(Vector3 angle) =>
            _label.text =
                $"Angle: x({Math.Round(angle.x, 2)}) y({Math.Round(angle.y, 2)}) z({Math.Round(angle.z, 2)})";
    }
}