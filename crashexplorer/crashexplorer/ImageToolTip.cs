/*
   This file is part of CrashExplorer.
   
   CrashExplorer is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.
   
   CrashExplorer is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
   GNU General Public License for more details.
   
   You should have received a copy of the GNU General Public License
   along with CrashExplorer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System.Drawing;
using System.Windows.Forms;

namespace CrashExplorer
{
  internal class ImageToolTip : ToolTip
  {
    private readonly Bitmap m_bitmap;
    public ImageToolTip(Bitmap bitmap)
    {
      m_bitmap = bitmap;
      OwnerDraw = true;
      Popup += OnPopup;
      Draw += OnDraw;
    }

    private void OnPopup(object sender, PopupEventArgs e)
    {
      e.ToolTipSize = new Size(m_bitmap.Width, m_bitmap.Height);
    }

    private void OnDraw(object sender, DrawToolTipEventArgs e)
    {
      e.Graphics.DrawImage(m_bitmap, new Rectangle(0, 0, m_bitmap.Width, m_bitmap.Height));
    }
  }
}
