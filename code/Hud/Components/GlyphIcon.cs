using Sandbox;
using Sandbox.UI;
namespace TerryTrials.Hud.Components;

public class GlyphIcon : Panel
{
	readonly Image GlyphImage;

	public GlyphIcon(InputButton button, InputGlyphSize size)
	{
		GlyphImage = new Image()
		{
			Texture = Input.GetGlyph( button, size ),
			Parent = this
		};

		GlyphImage.Style.Width = GlyphImage.Texture.Width;
		GlyphImage.Style.Height = GlyphImage.Texture.Height;
	}
}

