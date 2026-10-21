using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Pifagor.Core.CustomElements
{
    public class FlexPanel : Panel
    {
        public static readonly DependencyProperty GapProperty =
            DependencyProperty.Register("Gap", typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty MinChildWidthProperty =
            DependencyProperty.Register("MinChildWidth", typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public static readonly DependencyProperty MinChildHeightProperty =
            DependencyProperty.Register("MinChildHeight", typeof(double), typeof(FlexPanel),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsMeasure));

        public double Gap
        {
            get => (double)GetValue(GapProperty);
            set => SetValue(GapProperty, value);
        }

        public double MinChildWidth
        {
            get => (double)GetValue(MinChildWidthProperty);
            set => SetValue(MinChildWidthProperty, value);
        }

        public double MinChildHeight
        {
            get => (double)GetValue(MinChildHeightProperty);
            set => SetValue(MinChildHeightProperty, value);
        }
        protected override Size MeasureOverride(Size availableSize)
        {
            double gap = Gap;
            double minWidth = MinChildWidth;
            double minHeight = MinChildHeight;

            double currentLineWidth = 0;
            double currentLineHeight = 0;
            double totalHeight = 0;
            double maxWidth = 0;
            bool isFirstInLine = true;

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(availableSize);

                double childWidth = Math.Max(child.DesiredSize.Width, minWidth);
                double childHeight = Math.Max(child.DesiredSize.Height, minHeight);

                double neededWidth = isFirstInLine ? childWidth : childWidth + gap;

                if (currentLineWidth + neededWidth > availableSize.Width && !isFirstInLine)
                {
                    maxWidth = Math.Max(maxWidth, currentLineWidth);
                    totalHeight += currentLineHeight + gap;

                    currentLineWidth = childWidth;
                    currentLineHeight = childHeight;
                    isFirstInLine = true;
                }
                else
                {
                    currentLineWidth += neededWidth;
                    currentLineHeight = Math.Max(currentLineHeight, childHeight);
                    isFirstInLine = false;
                }
            }

            maxWidth = Math.Max(maxWidth, currentLineWidth);
            totalHeight += currentLineHeight;

            double finalWidth = double.IsPositiveInfinity(availableSize.Width) ? maxWidth : availableSize.Width;
            double finalHeight = double.IsPositiveInfinity(availableSize.Height) ? totalHeight : availableSize.Height;

            return new Size(finalWidth, finalHeight);
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            double gap = Gap;
            double minWidth = MinChildWidth;
            double minHeight = MinChildHeight;

            double currentY = 0;
            int currentIndex = 0;
            int totalChildren = InternalChildren.Count;

            while (currentIndex < totalChildren)
            {
                List<UIElement> lineChildren = new List<UIElement>();
                double totalDesiredWidth = 0;
                double maxLineHeight = 0;

                while (currentIndex < totalChildren)
                {
                    UIElement child = InternalChildren[currentIndex];

                    double childWidth = Math.Max(child.DesiredSize.Width, minWidth);

                    double potentialLineWidth = totalDesiredWidth + childWidth + (lineChildren.Count * gap);

                    if (potentialLineWidth > finalSize.Width && lineChildren.Count > 0)
                    {
                        break;
                    }

                    lineChildren.Add(child);
                    totalDesiredWidth += childWidth;

                    double childHeight = Math.Max(child.DesiredSize.Height, minHeight);
                    maxLineHeight = Math.Max(maxLineHeight, childHeight);

                    currentIndex++;
                }

                double totalGapsWidth = (lineChildren.Count - 1) * gap;
                double remainingWidth = finalSize.Width - totalDesiredWidth - totalGapsWidth;

                double extraWidthPerChild = remainingWidth > 0 ? remainingWidth / lineChildren.Count : 0;

                double currentX = 0;
                foreach (UIElement child in lineChildren)
                {
                    double baseWidth = Math.Max(child.DesiredSize.Width, minWidth);
                    double finalChildWidth = baseWidth + extraWidthPerChild;

                    child.Arrange(new Rect(currentX, currentY, finalChildWidth, maxLineHeight));

                    currentX += finalChildWidth + gap;
                }

                currentY += maxLineHeight + gap;
            }

            return finalSize;
        }
    }
}
