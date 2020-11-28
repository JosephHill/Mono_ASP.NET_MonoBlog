ifndef TARGET
	TARGET=./bin/Debug
else
	TARGET=./bin/$(TARGET)
endif

MCS=mcs
ifndef (RELEASE)
	MCSFLAGS=-debug --stacktrace
endif
LIBS=-lib:/usr/lib/mono/1.0 -lib:/usr/lib/mono/gtk-sharp


BLOGXRUNTIME_DLL=$(TARGET)/BlogXRuntime.dll
BLOGXRUNTIME_PDB=$(TARGET)/BlogXRuntime.pdb
BLOGXRUNTIME_SRC=AssemblyInfo.cs \
	BlogXData.cs \
	Collections.cs \
	DataModel.cs \
	FtpConnection.cs \
	FtpUpstreamer.cs \
	NavigationLink.cs \
	Rss.cs
BLOGXRUNTIME_RES=

$(BLOGXRUNTIME_DLL): $(BLOGXRUNTIME_SRC) 
	-mkdir -p $(TARGET)
	$(MCS) $(MCSFLAGS) $(LIBS) -r:System.dll -r:System.Data.dll -r:System.Xml.dll -target:library -out:$(BLOGXRUNTIME_DLL) $(BLOGXRUNTIME_RES) $(BLOGXRUNTIME_SRC)


# common targets

all:	$(BLOGXRUNTIME_DLL)

clean:
	-rm -f "$(BLOGXRUNTIME_DLL)" 2> /dev/null
	-rm -f "$(BLOGXRUNTIME_PDB)" 2> /dev/null


# project names as targets

BlogXRuntime: $(BLOGXRUNTIME_DLL)

