ifndef TARGET
	TARGET=./bin
else
	TARGET=./bin/$(TARGET)
endif

MCS=mcs
ifndef (RELEASE)
	MCSFLAGS=-debug --stacktrace
endif
LIBS=-lib:/usr/lib/mono/1.0 -lib:/usr/lib/mono/gtk-sharp


WEBLOGX_DLL=$(TARGET)/WeblogX.dll
WEBLOGX_PDB=$(TARGET)/WeblogX.pdb
WEBLOGX_SRC=AdminNavBar.ascx.cs \
	AssemblyInfo.cs \
	blogx.aspx.cs \
	BlogXBrowsing.asmx.cs \
	BlogXEditing.asmx.cs \
	BlogXFooter.ascx.cs \
	BlogXHeader.ascx.cs \
	BlogXUtils.cs \
	CategoryList.ascx.cs \
	CategoryView.aspx.cs \
	CommentView.aspx.cs \
	ConfigEditing.asmx.cs \
	default.aspx.cs \
	EntryEdit.aspx.cs \
	EntryView.ascx.cs \
	FormatControl.ascx.cs \
	FormatPage.aspx.cs \
	Global.asax.cs \
	Opml.cs \
	PermaLink.aspx.cs \
	Referrers.aspx.cs \
	SideBarList.ascx.cs \
	SideBarOpmlList.ascx.cs \
	SignIn.ascx.cs \
	SingleCommentView.ascx.cs \
	SiteConfig.cs \
	SiteSecurity.cs \
	VerifyImageGen.asmx.cs
WEBLOGX_RES=-resource:AdminNavBar.ascx.resx,Anderson.Chris.BlogX.WebClient.AdminNavBar.ascx.resx \
	-resource:blogx.aspx.resx,Anderson.Chris.BlogX.WebClient.blogx.aspx.resx \
	-resource:BlogXBrowsing.asmx.resx,Anderson.Chris.BlogX.WebClient.BlogXBrowsing.asmx.resx \
	-resource:BlogXEditing.asmx.resx,Anderson.Chris.BlogX.WebClient.BlogXEditing.asmx.resx \
	-resource:BlogXFooter.ascx.resx,Anderson.Chris.BlogX.WebClient.BlogXFooter.ascx.resx \
	-resource:BlogXHeader.ascx.resx,Anderson.Chris.BlogX.WebClient.BlogXHeader.ascx.resx \
	-resource:CategoryList.ascx.resx,Anderson.Chris.BlogX.WebClient.CategoryList.ascx.resx \
	-resource:CategoryView.aspx.resx,Anderson.Chris.BlogX.WebClient.CategoryView.aspx.resx \
	-resource:CommentView.aspx.resx,Anderson.Chris.BlogX.WebClient.CommentView.aspx.resx \
	-resource:ConfigEditing.asmx.resx,Anderson.Chris.BlogX.WebClient.ConfigEditing.asmx.resx \
	-resource:default.aspx.resx,Anderson.Chris.BlogX.WebClient.default.aspx.resx \
	-resource:EntryEdit.aspx.resx,Anderson.Chris.BlogX.WebClient.EntryEdit.aspx.resx \
	-resource:EntryView.ascx.resx,Anderson.Chris.BlogX.WebClient.EntryView.ascx.resx \
	-resource:FormatControl.ascx.resx,Anderson.Chris.BlogX.WebClient.FormatControl.ascx.resx \
	-resource:FormatPage.aspx.resx,Anderson.Chris.BlogX.WebClient.FormatPage.aspx.resx \
	-resource:Global.asax.resx,Anderson.Chris.BlogX.WebClient.Global.asax.resx \
	-resource:PermaLink.aspx.resx,Anderson.Chris.BlogX.WebClient.PermaLink.aspx.resx \
	-resource:Referrers.aspx.resx,Anderson.Chris.BlogX.WebClient.Referrers.aspx.resx \
	-resource:SideBarList.ascx.resx,Anderson.Chris.BlogX.WebClient.SideBarList.ascx.resx \
	-resource:SideBarOpmlList.ascx.resx,Anderson.Chris.BlogX.WebClient.SideBarOpmlList.ascx.resx \
	-resource:SignIn.ascx.resx,Anderson.Chris.BlogX.WebClient.SignIn.ascx.resx \
	-resource:SingleCommentView.ascx.resx,Anderson.Chris.BlogX.WebClient.SingleCommentView.ascx.resx \
	-resource:VerifyImageGen.asmx.resx,Anderson.Chris.BlogX.WebClient.VerifyImageGen.asmx.resx

$(WEBLOGX_DLL): $(WEBLOGX_SRC) # Missing dependency project BlogXRuntime ID:{2B6D1F60-C5D8-4CB9-89D6-A4AF42275852}?
	-mkdir -p $(TARGET)
	$(MCS) $(MCSFLAGS) $(LIBS) -r:System.dll -r:System.Drawing.dll -r:System.Data.dll -r:System.Web.dll -r:System.Xml.dll -r:BlogXRuntime.dll -r:System.Web.Services.dll -target:library -out:$(WEBLOGX_DLL) $(WEBLOGX_RES) $(WEBLOGX_SRC)


# common targets

all:	$(WEBLOGX_DLL)

clean:
	-rm -f "$(WEBLOGX_DLL)" 2> /dev/null
	-rm -f "$(WEBLOGX_PDB)" 2> /dev/null


# project names as targets

WeblogX: $(WEBLOGX_DLL)

