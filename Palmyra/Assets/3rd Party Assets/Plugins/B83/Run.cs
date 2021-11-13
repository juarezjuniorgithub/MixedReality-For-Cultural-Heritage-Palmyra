using System;
using UnityEngine;
using System.Collections;

namespace B83 {
	public class Run {
		public bool isDone;
		public bool abort;
		private IEnumerator action;
		public Action onGUIaction;

		//preallocate wait objects
		private static readonly WaitForSeconds waitForTenthSecond = new WaitForSeconds(0.1f);
		private static readonly WaitForSeconds waitForQuarterSecond = new WaitForSeconds(0.25f);
		private static readonly WaitForSeconds waitForHalfSecond = new WaitForSeconds(0.5f);
		private static readonly WaitForSeconds waitForOneSecond = new WaitForSeconds(1.0f);

		#region Run.EachFrame

		public static Run EachFrame(Action aAction) {
			var tmp = new Run();
			tmp.action = _RunEachFrame(tmp, aAction);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunEachFrame(Run aRun, Action aAction) {
			aRun.isDone = false;
			while (true) {
				if (!aRun.abort && aAction != null) {
					aAction();
				}
				else {
					break;
				}

				yield return null;
			}

			aRun.isDone = true;
		}

		#endregion Run.EachFrame

		#region Run.Every

		/// <summary>
		/// Run aAction every aDelay seconds
		/// </summary>
		public static Run Every(float aInitialDelay, float aDelay, Action aAction) {
			var tmp = new Run();

			WaitForSeconds wait = null;
			if (Math.Abs(aDelay - 0.1f) < float.Epsilon) {
				wait = waitForTenthSecond;
			}
			else if (Math.Abs(aDelay - 0.25f) < float.Epsilon) {
				wait = waitForQuarterSecond;
			}
			else if (Math.Abs(aDelay - 0.5f) < float.Epsilon) {
				wait = waitForHalfSecond;
			}
			else if (Math.Abs(aDelay - 1.0f) < float.Epsilon) {
				wait = waitForOneSecond;
			}

			tmp.action = _RunEvery(tmp, aInitialDelay, aDelay, aAction, wait);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunEvery(Run aRun, float aInitialDelay, float aSeconds, Action aAction,
			WaitForSeconds wait = null) {
			aRun.isDone = false;
			if (wait == null) {
				wait = new WaitForSeconds(aSeconds);
			}

			if (aInitialDelay > 0f) {
				yield return new WaitForSeconds(aInitialDelay);
			}
			else {
				var FrameCount = Mathf.RoundToInt(-aInitialDelay);
				for (var i = 0; i < FrameCount; i++)
					yield return null;
			}

			while (true) {
				if (!aRun.abort && aAction != null) {
					aAction();
				}
				else {
					break;
				}

				if (aSeconds > 0) {
					yield return wait;
				}
				else {
					var FrameCount = Mathf.Max(1, Mathf.RoundToInt(-aSeconds));
					for (var i = 0; i < FrameCount; i++)
						yield return null;
				}
			}

			aRun.isDone = true;
		}

		#endregion Run.Every

		#region Run.After

		/// <summary>
		/// Run aAction after given timeout. 
		/// </summary>
		/// <param name="aDelay">Timeout</param>
		/// <param name="aAction">the action to run</param>
		/// <param name="realtime">don't wait in Unity frame time but actual seconds</param>
		public static Run After(float aDelay, Action aAction, bool realtime = false) {
			var tmp = new Run();
			tmp.action = realtime ? _RunAfterRealtime(tmp, aDelay, aAction) : _RunAfter(tmp, aDelay, aAction);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunAfter(Run aRun, float aDelay, Action aAction) {
			aRun.isDone = false;
			yield return new WaitForSeconds(aDelay);
			if (!aRun.abort) {
				aAction?.Invoke();
			}

			aRun.isDone = true;
		}

		#endregion Run.After

		#region Run.AfterRealtime

		private static IEnumerator _RunAfterRealtime(Run aRun, float aDelay, Action aAction) {
			aRun.isDone = false;
			yield return new WaitForSecondsRealtime(aDelay);
			if (!aRun.abort) {
				aAction?.Invoke();
			}

			aRun.isDone = true;
		}

		#endregion Run.After

		#region Run.NextFrame

		public static Run NextFrame(Action aAction) {
			var tmp = new Run();
			tmp.action = _RunNextFrame(tmp, aAction);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunNextFrame(Run aRun, Action aAction) {
			aRun.isDone = false;
			yield return 0;
			if (!aRun.abort) {
				aAction?.Invoke();
			}

			aRun.isDone = true;
		}

		#endregion

		#region Run.Lerp

		public static Run Lerp(float aDuration, Action<float> aAction) {
			var tmp = new Run();
			tmp.action = _RunLerp(tmp, aDuration, aAction);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunLerp(Run aRun, float aDuration, Action<float> aAction) {
			aRun.isDone = false;
			var t = 0f;
			while (t < 1.0f) {
				t = Mathf.Clamp01(t + Time.deltaTime / aDuration);
				if (!aRun.abort) {
					aAction?.Invoke(t);
				}

				yield return null;
			}

			aRun.isDone = true;

		}

		#endregion Run.Lerp

		#region Run.OnDelegate

		public static Run OnDelegate(SimpleEvent aDelegate, Action aAction) {
			var tmp = new Run();
			tmp.action = _RunOnDelegate(tmp, aDelegate, aAction);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _RunOnDelegate(Run aRun, SimpleEvent aDelegate, Action aAction) {
			aRun.isDone = false;
			var action = aAction;
			aDelegate.Add(action);
			while (!aRun.abort && aAction != null) {
				yield return null;
			}

			aDelegate.Remove(action);
			aRun.isDone = true;
		}

		#endregion Run.OnDelegate

		#region Run.Coroutine

		public static Run Coroutine(IEnumerator aCoroutine) {
			var tmp = new Run();
			tmp.action = _Coroutine(tmp, aCoroutine);
			tmp.Start();
			return tmp;
		}

		private static IEnumerator _Coroutine(Run aRun, IEnumerator aCoroutine) {
			yield return CoroutineHelper.Instance.StartCoroutine(aCoroutine);
			aRun.isDone = true;
		}

		#endregion Run.Coroutine

		public static Run OnGUI(float aDuration, Action aAction) {
			var tmp = new Run {onGUIaction = aAction};
			if (aDuration > 0.0f) {
				tmp.action = _RunAfter(tmp, aDuration, null);
			}
			else {
				tmp.action = null;
			}

			tmp.Start();
			CoroutineHelper.Instance.Add(tmp);
			return tmp;
		}

		public class CTempWindow {
			public Run inst;
			public Rect pos;
			public string title;
			public int winID = GUIHelper.GetFreeWindowID();

			public void Close() {
				inst.Abort();
			}
		}

		public static CTempWindow CreateGUIWindow(Rect aPos, string aTitle, Action<CTempWindow> aAction) {
			CTempWindow tmp = new CTempWindow {pos = aPos, title = aTitle};
			tmp.inst = OnGUI(0,
				() => { tmp.pos = GUI.Window(tmp.winID, tmp.pos, (id) => { aAction(tmp); }, tmp.title); });
			return tmp;
		}


		private void Start() {
			if (action != null) {
				CoroutineHelper.Instance.StartCoroutine(action);
			}
		}

		public Coroutine WaitFor => CoroutineHelper.Instance.StartCoroutine(_WaitFor(null));

		public IEnumerator _WaitFor(Action aOnDone) {
			while (!isDone) {
				yield return null;
			}

			aOnDone?.Invoke();
		}

		public void Abort() {
			abort = true;
		}

		public Run ExecuteWhenDone(Action aAction) {
			var tmp = new Run {action = _WaitFor(aAction)};
			tmp.Start();
			return tmp;
		}
	}
}