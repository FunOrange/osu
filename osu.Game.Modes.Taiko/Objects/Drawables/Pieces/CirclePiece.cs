// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Game.Graphics.Backgrounds;
using OpenTK.Graphics;

namespace osu.Game.Modes.Taiko.Objects.Drawables.Pieces
{
    /// <summary>
    /// A circle piece which is used uniformly through osu!taiko to visualise hitobjects.
    /// <para>
    /// Note that this can actually be non-circle if the width is changed. See <see cref="ElongatedCirclePiece"/>
    /// for a usage example.
    /// </para>
    /// </summary>
    public class CirclePiece : TaikoPiece
    {
        public const float SYMBOL_SIZE = TaikoHitObject.CIRCLE_RADIUS * 2f * 0.45f;
        public const float SYMBOL_BORDER = 8;
        public const float SYMBOL_INNER_SIZE = SYMBOL_SIZE - 2 * SYMBOL_BORDER;

        /// <summary>
        /// The amount to scale up the base circle to show it as a "strong" piece.
        /// </summary>
        protected const float STRONG_SCALE = 1.5f;

        /// <summary>
        /// The colour of the inner circle and outer glows.
        /// </summary>
        public override Color4 AccentColour
        {
            get { return base.AccentColour; }
            set
            {
                base.AccentColour = value;

                background.Colour = AccentColour;

                resetEdgeEffects();
            }
        }

        /// <summary>
        /// Whether Kiai mode effects are enabled for this circle piece.
        /// </summary>
        public override bool KiaiMode
        {
            get { return base.KiaiMode; }
            set
            {
                base.KiaiMode = value;

                resetEdgeEffects();
            }
        }

        protected override Container<Drawable> Content => content;

        private readonly Container content;

        private readonly Container background;

        public CirclePiece(bool isStrong = false)
        {
            AddInternal(new Drawable[]
            {
                background = new CircularContainer
                {
                    Name = "Background",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Masking = true,
                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                        },
                        new Triangles
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            ColourLight = Color4.White,
                            ColourDark = Color4.White.Darken(0.1f)
                        }
                    }
                },
                new CircularContainer
                {
                    Name = "Ring",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    BorderThickness = 8,
                    BorderColour = Color4.White,
                    Masking = true,
                    Children = new[]
                    {
                        new Box
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            RelativeSizeAxes = Axes.Both,
                            Alpha = 0,
                            AlwaysPresent = true
                        }
                    }
                },
                content = new Container
                {
                    RelativeSizeAxes = Axes.Both,
                    Name = "Content",
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                }
            });

            if (isStrong)
            {
                Size *= STRONG_SCALE;

                //default for symbols etc.
                Content.Scale *= STRONG_SCALE;
            }
        }

        private void resetEdgeEffects()
        {
            background.EdgeEffect = new EdgeEffect
            {
                Type = EdgeEffectType.Glow,
                Colour = AccentColour,
                Radius = KiaiMode ? 50 : 8
            };
        }
    }
}