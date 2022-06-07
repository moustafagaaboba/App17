using System;

namespace App17.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);
}
