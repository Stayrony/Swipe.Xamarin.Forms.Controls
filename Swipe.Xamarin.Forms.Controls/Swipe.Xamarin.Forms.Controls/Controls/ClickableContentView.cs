using System;
using System.Windows.Input;
using NControl.Abstractions;
using Xamarin.Forms;

namespace Swipe.Xamarin.Forms.Controls.Controls
{
	public class ClickableContentView : NControlView
	{
		private State _currentState = State.Default;
		protected State CurrentState
		{
			get { return _currentState; }
			set
			{
				if (_currentState != value)
				{
					_currentState = value;
					UpdateState();
				}
			}
		}

		#region -- Public properties --

		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ClickableContentView), default(ICommand));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly BindableProperty CommandParameterProperty =
					BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ClickableContentView), default(object));

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly BindableProperty SelectedScaleProperty =
					BindableProperty.Create(nameof(SelectedScale), typeof(double), typeof(ClickableContentView), 0.95);


		public double SelectedScale
		{
			get { return (double)GetValue(SelectedScaleProperty); }
			set { SetValue(SelectedScaleProperty, value); }
		}

		protected virtual void UpdateState()
		{
			if (this.IsEnabled)
			{
				if (Math.Abs(SelectedScale - 1.0f) > 0.001)
				{
					uint animationMiliseconds = 150;
					if (CurrentState == State.Selected)
					{
						this.ScaleTo(SelectedScale, animationMiliseconds);
					}
					else
					{
						if (Math.Abs(Scale - 1.0f) > 0.001)
							this.ScaleTo(1.0f);
					}
				}
			}
		}

		#endregion

		#region -- Overrides --

		public override bool TouchesBegan(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			base.TouchesBegan(points);
			CurrentState = State.Selected;
			return true;
		}

		public override bool TouchesCancelled(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			base.TouchesCancelled(points);
			CurrentState = State.Default;
			return true;
		}

		public override bool TouchesEnded(System.Collections.Generic.IEnumerable<NGraphics.Point> points)
		{
			base.TouchesEnded(points);
			var isTouchEndedInside = false;
			foreach (var item in points)
			{
				if ((this.Bounds.Width >= item.X && item.X >= 0) &&
					(this.Bounds.Height >= item.Y && item.Y >= 0))
					isTouchEndedInside = true;
			}
			if (isTouchEndedInside)
			{
				OnClicked();
			}
			CurrentState = State.Default;
			return true;
		}

		#endregion

		#region -- Private helpers --

		private void OnClicked()
		{
			if (Command != null && IsEnabled && Command.CanExecute(CommandParameter))
				Command?.Execute(CommandParameter);
		}

		#endregion

		protected enum State
		{
			Default,
			Selected,
			Disabled
		}
	}
}
