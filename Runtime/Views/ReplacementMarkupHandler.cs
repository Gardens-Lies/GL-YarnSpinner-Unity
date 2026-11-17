/*
Yarn Spinner is licensed to you under the terms found in the file LICENSE.md.
*/

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Yarn.Markup;

#nullable enable

namespace Yarn.Unity
{
    /// <summary>
    /// An attribute marker processor receives a marker found in a Yarn line,
    /// and optionally rewrites the marker and its children into a new form.
    /// </summary>
    /// <seealso cref="LineProviderBehaviour"/>
    public abstract class ReplacementMarkupHandler : MonoBehaviour, IAttributeMarkerProcessor
    {

        /// <inheritdoc/>
        public abstract ReplacementMarkerResult ProcessReplacementMarker(MarkupAttribute marker, StringBuilder childBuilder, List<MarkupAttribute> childAttributes, string localeCode);

        /// <summary>
        /// Registers this handler's markers on its line provider (see <see cref="FindLineProvider"/>).
        /// </summary>
        /// <remarks>Called each time this component is enabled. Call it again
        /// if the line provider of the dialogue runner changes.</remarks>
        public abstract void RegisterMarker();

        /// <summary>
        /// Called by Unity when this component is enabled, right after <c>Awake</c> on
        /// instantiation: markers are registered before the next line is parsed.
        /// </summary>
        /// <remarks>A subclass that overrides this method must call it.</remarks>
        protected virtual void OnEnable()
        {
            RegisterMarker();
        }

        /// <summary>
        /// Gets the line provider to register markers on.
        /// </summary>
        /// <param name="configuredProvider">The line provider set in the Inspector, if any.</param>
        /// <returns><paramref name="configuredProvider"/> if it's set, otherwise the line
        /// provider of this handler's dialogue runner, or <see langword="null"/>.</returns>
        protected LineProviderBehaviour? FindLineProvider(LineProviderBehaviour? configuredProvider)
        {
            if (configuredProvider != null)
            {
                return configuredProvider;
            }
            var runner = DialogueRunner.FindRunner(this);
            return runner != null ? runner.LineProvider as LineProviderBehaviour : null;
        }
    }
}
