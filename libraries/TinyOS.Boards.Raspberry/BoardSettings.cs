﻿namespace TinyOS.Hosting
{
    public static partial class BoardSettings
    {
        // internal use only
        public static readonly string BoardType = "_board:type_";
        public static readonly string ControllerDefault = "_controller:default_";

        // public configuration settings
        public static readonly string TimeZoneOffset = "timezone:offset";
        public static readonly string MinimumLogLevel = "logging:log-level";
        public static readonly string NetworkTimeServer = "ntp:server";
        public static readonly string NetworkTimeEnabled = "ntp:ntp-enabled";
    }
}