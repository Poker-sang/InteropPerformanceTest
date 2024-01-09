// dllmain.h: 模块类的声明。

class CATLProjectModule : public ATL::CAtlDllModuleT< CATLProjectModule >
{
public :
	DECLARE_LIBID(LIBID_ATLProjectLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_ATLPROJECT, "{03f9fcbc-325d-4267-9a56-dd3c43b4ef65}")
};

extern class CATLProjectModule _AtlModule;
