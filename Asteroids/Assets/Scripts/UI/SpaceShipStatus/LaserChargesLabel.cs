using Infrastructure.Services;
using Infrastructure.Services.ServiceLocator;
using Infrastructure.Services.SpaceShipDataUpdate;
using TMPro;
using UnityEngine;

namespace UI.SpaceShipStatus
{
    [RequireComponent(typeof(TMP_Text))]
    public class LaserChargesLabel : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private TMP_Text _label;

        private void Start()
        {
            _label = GetComponent<TMP_Text>();

            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.CountOfLaserUpdated += UpdateText;
        }

        private void UpdateText(int laser) =>
            _label.text =
                $"Laser charges: {laser}";
    }
}