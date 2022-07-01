using Infrastructure.Services;
using Infrastructure.Services.SpaceShipDataUpdate;
using TMPro;
using UnityEngine;

namespace UI.SpaceShipStatus
{
    [RequireComponent(typeof(TMP_Text))]
    public class BulletsCountLabel : MonoBehaviour
    {
        private ISpaceShipDataUpdater _spaceShipDataUpdater;

        private TMP_Text _label;

        private void Start()
        {
            _label = GetComponent<TMP_Text>();

            _spaceShipDataUpdater = AllServices.Container.Single<ISpaceShipDataUpdater>();
            _spaceShipDataUpdater.CountOfBulletsUpdated += UpdateText;
        }

        private void UpdateText(int bullets) =>
            _label.text =
                $"Bullets: {bullets}";
    }
}