# AutoGetReferenced SOLIDWORKS Addin

When opening a file in SOLIDWORKS, this add-in looks at the EPDM version numbers of all referenced documents (at the time the file being opened was last checked in) and attempts to retrieve those versions from the archive server and place them in the local cache.  If the version cannot be retrieved (usually because a different version is already open or another document is open that references a different version) the user will be notified and can choose to cancel the opening of the file, or continue opening the file with the referenced versions that already in the local cache.

Get the latest Windows Installer file [here](https://github.com/jsculley/AutoGetReferencedAddin/releases/download/0.1/AutoGetReferenced.Addin.Setup.msi).
