﻿using System;

namespace FreeSecur.API.Core.GeneralHelpers
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }

        DateTime GetDateTime(int year, int month, int day);
        DateTime GetDateTime(int year, int month, int day, int hour, int minute, int second);
    }
}