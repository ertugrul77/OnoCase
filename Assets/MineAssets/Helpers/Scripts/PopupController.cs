using DG.Tweening;
using MineAssets.Helpers.Scripts;
using UnityEngine;

	[RequireComponent(typeof(RectTransform))]
	public class PopupController : MonoBehaviour
	{
		public PopupType type;
		public Canvas enclosingCanvas;
		private RectTransform _rectTransform;

		private void Start()
		{
			_rectTransform = GetComponent<RectTransform>();
			_rectTransform.localScale = Vector3.zero;
		}

		public void OpenPopup()
		{
			enclosingCanvas.enabled = true;
			_rectTransform.DOScale(Vector3.one, .4f).SetEase(Ease.OutExpo);
		}

		public void ClosePopup()
		{
			_rectTransform.DOScale(Vector3.zero, .4f)
				.SetEase(Ease.OutExpo).OnComplete(() => {
					enclosingCanvas.enabled = false;

					if (type == PopupType.Settings)
					{
						EventManager2.Instance.CloseSettingsPopup();
					}
				});
		}
	}

	public enum PopupType
	{
		Settings,
		Ordinary
	}
