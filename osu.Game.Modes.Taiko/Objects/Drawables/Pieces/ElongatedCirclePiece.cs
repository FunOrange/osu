// Copyright (c) 2007-2017 ppy Pty Ltd <contact@ppy.sh>.
// Licensed under the MIT Licence - https://raw.githubusercontent.com/ppy/osu/master/LICENCE

using System;
using osu.Framework.Graphics.Primitives;
using osu.Game.Modes.Taiko.UI;

namespace osu.Game.Modes.Taiko.Objects.Drawables.Pieces
{
    public class ElongatedCirclePiece : CirclePiece
    {
        /// <summary>
        /// As we are being used to define the absolute size of hits, we need to be given a relative reference of our containing <see cref="TaikoPlayfield"/>.
        /// </summary>
        public Func<float> PlayfieldLengthReference;

        /// <summary>
        /// The length of this piece as a multiple of the value returned by <see cref="PlayfieldLengthReference"/>
        /// </summary>
        public float Length;

        public ElongatedCirclePiece(bool isStrong = false) : base(isStrong)
        {
            if (isStrong)
            {
                //undo the strong scale provided in CirclePiece.
                Content.Scale /= STRONG_SCALE;
            }
        }

        protected override void Update()
        {
            base.Update();

            Content.Padding = new MarginPadding
            {
                Left = DrawHeight / 2,
                Right = DrawHeight / 2,
            };

            Width = (PlayfieldLengthReference?.Invoke() ?? 0) * Length + DrawHeight;
        }
    }
}