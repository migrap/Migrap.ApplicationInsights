using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Migrap.ApplicationInsights {
    public delegate Func<string> TrackEventName(TelemetryClient client = null);

    public static class TelemetryClientExtensions {
        public static void TrackEvent(this TelemetryClient client, Func<TelemetryClient, Func<string>> name, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null) {
            client.TrackEvent(name(client)(), properties, metrics);
        }

        public static void TrackEvent(this TelemetryClient client, Func<TelemetryClient, string> name, IDictionary<string, string> properties = null, IDictionary<string, double> metrics = null) {
            client.TrackEvent(name(client), properties, metrics);
        }

        public static void TrackEvent(this TelemetryClient client, string name, object properties = null, IDictionary<string, double> metrics = null) {
            client.TrackEvent(name, properties.AsDictionary(), metrics);
        }

        public static void TrackEvent(this TelemetryClient client, Func<TelemetryClient, Func<string>> name, object properties = null, IDictionary<string, double> metrics = null) {
            client.TrackEvent(name(client)(), properties.AsDictionary(), metrics);
        }

        public static void TrackEvent(this TelemetryClient client, Func<TelemetryClient, string> name, object properties = null, IDictionary<string, double> metrics = null) {
            client.TrackEvent(name(client), properties.AsDictionary(), metrics);
        }

        private static IDictionary<string, string> AsDictionary(this object value) {
            return value.GetType().GetProperties().ToDictionary(x => x.Name, x => x.GetValue(value).ToString());
        }
    }
}