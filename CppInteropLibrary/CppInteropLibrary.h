#pragma once
#include <Windows.h>
#include <VersionHelpers.h>

using namespace System;
using namespace System::Runtime::InteropServices;

namespace CppInteropLibrary {

    public ref class CppInteropClass sealed
    {
    public:
        HRESULT static Test(INT executions)
        {
            for (int i = 0; i < executions; ++i)
            {
                IsWindows10OrGreater();
            }
            return S_OK;
        }
    };
}
