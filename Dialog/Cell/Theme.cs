//
// Theme.cs
//
// Author:
//   Robert Kozak (rkozak@gmail.com) Twitter:@robertkozak
//
// Copyright 2011, Nowcom Corporation
//
// Code licensed under the MIT X11 license
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
namespace MonoMobile.MVVM
{
	using System;
	using System.Drawing;
	using MonoTouch.CoreGraphics;
	using MonoTouch.Dialog;
	using MonoTouch.UIKit;

	public class Theme
	{
		private UITableViewElementCell _Cell;

		private UIImage _CellImageIcon;
		private Uri _CellImageIconUri;

		private UIImage _CellBackgroundImage;
		private Uri _CellBackgroundUri;
		private UIColor _CellBackgroundColor;
		
		private UIColor _TextColor;
		private UIColor _DetailTextColor;
		
		public string Name { get; set; }
		public bool DrawWhenHighlighted { get; set; }

		public UIColor PlaceholderColor { get; set; }
		public UITextAlignment PlaceholderAlignment { get; set; }

		public UIColor SeparatorColor { get; set; }
		public UITableViewCellSeparatorStyle? SeparatorStyle { get; set; }
		public UITableViewStyle? TableViewStyle { get; set; }

		public UIBarStyle? BarStyle { get; set; }
		public UIColor BarTintColor { get; set; }
		public string BarImage { get; set; }
		public bool BarTranslucent { get; set; }
		
		public UITableViewElementCell Cell 
		{ 
			get {return _Cell; }
			set { InitializePropertiesFromCell(value); } 
		}
		
		public UITableViewCellStyle CellStyle { get; set; }
		public UITableViewCellAccessory Accessory { get; set; }

		public UIImage CellImageIcon
		{
			get { return _CellImageIcon; }
			set
			{
				_CellImageIcon = value;
				_CellImageIconUri = null;
			}
		}

		public Uri CellImageIconUri
		{
			get { return _CellImageIconUri; }
			set {
				_CellImageIconUri = value;
				
				if (_CellImageIcon != null)
				{
					_CellImageIcon.Dispose();
					_CellImageIcon = null;
				}
			}
		}

		public UIImage CellBackgroundImage
		{
			get { return _CellBackgroundImage; }
			set {
				_CellBackgroundImage = value;
				_CellBackgroundUri = null;
				_CellBackgroundColor = null;
			}
		}

		public Uri CellBackgroundUri
		{
			get { return _CellBackgroundUri; }
			set {
				_CellBackgroundUri = value;
				_CellBackgroundColor = null;
				
				if (_CellBackgroundImage != null)
				{
					_CellBackgroundImage.Dispose();
					_CellBackgroundImage = null;
				}
			}
		}

		public UIColor CellBackgroundColor
		{
			get { return _CellBackgroundColor; }
			set {
				_CellBackgroundColor = value;
				_CellBackgroundUri = null;
				
				ClearBackground();
				
				if (_CellBackgroundImage != null)
				{
					_CellBackgroundImage.Dispose();
					_CellBackgroundImage = null;
				}
			}
		}
		
		public UILabel TextLabel { get; set; }
		public UIFont TextFont { get; set; }
		public UITextAlignment TextAlignment { get; set; }
		public SizeF TextShadowOffset { get; set; }
		public UIColor TextShadowColor { get; set; }

		public UILabel DetailTextLabel { get; set; }
		public UIFont DetailTextFont { get; set; }
		public UITextAlignment DetailTextAlignment { get; set; }
		public SizeF DetailTextShadowOffset { get; set; }
		public UIColor DetailTextShadowColor { get; set; }

		public UIColor TextColor
		{
			get { return _TextColor; }
			set {
				_TextColor = value;
				ClearBackground();
			}
		}
		
		public UIColor DetailTextColor
		{
			get { return _DetailTextColor; }
			set {
				_DetailTextColor = value;
				ClearBackground();
			}
		}
		
		public UILabel HeaderTextLabel { get; set; }
		public UIFont HeaderTextFont { get; set; }
		public UITextAlignment HeaderTextAlignment { get; set; }
		public UIColor HeaderTextColor { get; set; }
		public SizeF HeaderTextShadowOffset { get; set; }
		public UIColor HeaderTextShadowColor { get; set; }

		public UILabel FooterTextLabel { get; set; }
		public UIFont FooterTextFont { get; set; }
		public UITextAlignment FooterTextAlignment { get; set; }
		public UIColor FooterTextColor { get; set; }
		public SizeF FooterTextShadowOffset { get; set; }
		public UIColor FooterTextShadowColor { get; set; }
		
		public Action<RectangleF, CGContext, UITableViewElementCell> DrawContentViewAction { get; set; }

		public Theme()
		{
			PlaceholderAlignment = UITextAlignment.Right;
			DetailTextAlignment = UITextAlignment.Right;
			TextAlignment = UITextAlignment.Left;

			Accessory = UITableViewCellAccessory.None;
		}
		
		public static Theme CreateTheme(Theme theme)
		{
			var newTheme = new Theme();

			newTheme.MergeTheme(theme);

			return newTheme;
		}
		
		public void MergeTheme(Theme theme)
		{
			if (theme != null)
			{	
				Name = theme.Name;

				if (theme.CellStyle != UITableViewCellStyle.Default && CellStyle != theme.CellStyle)
					CellStyle = theme.CellStyle;
				
				if (theme.Accessory != Accessory)
					Accessory = theme.Accessory;
				
				if (theme.CellImageIcon != null)
					CellImageIcon = theme.CellImageIcon;
				
				if (theme.CellImageIconUri != null)
					CellImageIconUri = theme.CellImageIconUri;
				
				if (theme.CellBackgroundColor != null)
					CellBackgroundColor = theme.CellBackgroundColor;
				
				if (theme.CellBackgroundUri != null)
					CellBackgroundUri = theme.CellBackgroundUri;
				
				if (theme.CellBackgroundImage != null)
					CellBackgroundImage = theme.CellBackgroundImage;
				
				TextAlignment = theme.TextAlignment;
				
				if (theme.TextFont != null)
					TextFont = theme.TextFont;
				
				if (theme.TextColor != null)
					TextColor = theme.TextColor;
				
				if (theme.TextShadowOffset != SizeF.Empty)
					TextShadowOffset = theme.TextShadowOffset;
				
				if (theme.TextShadowColor != null)
					TextShadowColor = theme.TextShadowColor;
				
				if (theme.TextLabel != null)
					TextLabel = theme.TextLabel;
				
		
				DetailTextAlignment = theme.DetailTextAlignment;
				
				if (theme.DetailTextFont != null)
					DetailTextFont = theme.DetailTextFont;
				
				if (theme.DetailTextColor != null)
					DetailTextColor = theme.DetailTextColor;
				
				if (theme.DetailTextShadowOffset != SizeF.Empty)
					TextShadowOffset = theme.DetailTextShadowOffset;
				
				if (theme.DetailTextShadowColor != null)
					TextShadowColor = theme.DetailTextShadowColor;
				
				if (theme.DetailTextLabel != null)
					DetailTextLabel = theme.DetailTextLabel;
				
	
				if (theme.PlaceholderColor != null)
					PlaceholderColor = theme.PlaceholderColor;
				
				if (theme.PlaceholderAlignment != UITextAlignment.Right)
					PlaceholderAlignment = theme.PlaceholderAlignment;

				if (theme.SeparatorColor != null)
					SeparatorColor = theme.SeparatorColor;

				if (theme.SeparatorStyle.HasValue)
					SeparatorStyle = theme.SeparatorStyle;

				if (theme.TableViewStyle.HasValue)
					TableViewStyle = theme.TableViewStyle;				

				if (theme.BarStyle.HasValue)
					BarStyle = theme.BarStyle;

				if (theme.BarTintColor != null)
					BarTintColor = theme.BarTintColor;
		
				if (theme.BarImage != null)
					BarImage = theme.BarImage;
		
				if (theme.BarTranslucent)
					BarTranslucent = theme.BarTranslucent;

			
				HeaderTextAlignment = theme.HeaderTextAlignment;
				
				if (theme.HeaderTextFont != null)
					HeaderTextFont = theme.HeaderTextFont;
				
				if (theme.HeaderTextColor != null)
					HeaderTextColor = theme.HeaderTextColor;
				
				if (theme.HeaderTextShadowOffset != SizeF.Empty)
					HeaderTextShadowOffset = theme.HeaderTextShadowOffset;
				
				if (theme.HeaderTextShadowColor != null)
					HeaderTextShadowColor = theme.HeaderTextShadowColor;
				
				if (theme.HeaderTextLabel != null)
					HeaderTextLabel = theme.HeaderTextLabel;


				FooterTextAlignment = theme.FooterTextAlignment;
				
				if (theme.FooterTextFont != null)
					FooterTextFont = theme.FooterTextFont;
				
				if (theme.FooterTextColor != null)
					FooterTextColor = theme.FooterTextColor;
				
				if (theme.FooterTextShadowOffset != SizeF.Empty)
					FooterTextShadowOffset = theme.FooterTextShadowOffset;
				
				if (theme.FooterTextShadowColor != null)
					FooterTextShadowColor = theme.FooterTextShadowColor;
				
				if (theme.FooterTextLabel != null)
					FooterTextLabel = theme.FooterTextLabel;

				if (theme.DrawContentViewAction != null)
				{
					DrawContentViewAction = theme.DrawContentViewAction;
				}
			}
		}

		private void InitializePropertiesFromCell(UITableViewElementCell cell)
		{
			if (cell != null)
			{
				_Cell = cell;
 
				if (Cell.Accessory != UITableViewCellAccessory.None && Accessory == UITableViewCellAccessory.None)
					Accessory = Cell.Accessory;

				if (Cell.ImageView != null && CellImageIcon == null)
					CellImageIcon = Cell.ImageView.Image;
				
				if (TextLabel != null)
				{
					if (TextFont == null)
						TextFont = cell.TextLabel.Font;
	
					if (TextColor == null)
						TextColor = cell.TextLabel.TextColor;
				
					if (TextShadowOffset == SizeF.Empty)
						TextShadowOffset = cell.TextLabel.ShadowOffset;
	
					if (TextShadowColor == null)
						TextShadowColor = cell.TextLabel.ShadowColor;
				}
				else
					TextLabel = Cell.TextLabel;

				if (DetailTextLabel != null)
				{
					if (DetailTextFont == null)
						DetailTextFont = cell.DetailTextLabel.Font;
					
					if (DetailTextColor == null)
						DetailTextColor = cell.DetailTextLabel.TextColor;
					
					if (DetailTextShadowOffset == SizeF.Empty)
						DetailTextShadowOffset = cell.DetailTextLabel.ShadowOffset;
					
					if (DetailTextShadowColor == null)
						DetailTextShadowColor = cell.DetailTextLabel.ShadowColor;
				}
				else
					DetailTextLabel = Cell.DetailTextLabel;
				
				Cell.SetNeedsDisplay();
			}
		}

		public void ClearBackground()
		{
			if (TextLabel != null)
				TextLabel.BackgroundColor = UIColor.Clear;
			
			if (DetailTextLabel != null)
				DetailTextLabel.BackgroundColor = UIColor.Clear;
		}
	}
}

