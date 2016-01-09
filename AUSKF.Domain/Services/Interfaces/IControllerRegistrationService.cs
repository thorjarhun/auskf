﻿namespace AUSKF.Domain.Services.Interfaces
{
    public interface IControllerRegistrationService
    {
        /// <summary>
        /// Registers the controllers.
        /// Needs to be run early in setup.
        /// </summary>
        void RegisterControllers();
    }
}