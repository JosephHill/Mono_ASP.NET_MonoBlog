Name:           Mono_ASP.NET_MonoBlog
Version:        0.24
Release:        1
License:        Shared Source License for ASP.NET Source Projects
Source:         BlogX-24_1.tar.gz
BuildRoot:      %{_tmppath}/%{name}-%{version}-build
BuildRequires:  mono-web xsp
BuildArch:      noarch  
Group:          Mono/ASP.NET
Summary:        BlogX ASP.NET 1.1 weblog engine

%define aspPrefix /usr/share/mono/asp.net  
%define appsPrefix %{aspPrefix}/apps  
%define appsDataPrefix %{aspPrefix}/data  
%define appLocation %{appsPrefix}/%{name}
%define appDataLocation %{appsDataPrefix}/%{name}
%define appVirtualPath /IBuySpyPortal 
%define appInstanceName IBuySpy Portal
  
%define xspConfigsLocation /etc/xsp/1.0  
%define xspAvailableApps %{xspConfigsLocation}/applications-available  
%define xspEnabledApps %{xspConfigsLocation}/applications-enabled  

%description
Mono Blog is a Mono-compatible version of the BlogX weblog engine originally developed by Chris Anderson. Because BlogX is written in C# and uses XML to store content, it is one of the easiest complete, open-source ASP.NET 1.1 applications to get running on Mono.

%prep
%setup -q -n BlogX

%build
make

%clean
rm -rf "$RPM_BUILD_ROOT"

%files
%defattr(-, root, root)
%{appLocation}
%{appDataLocation}
%{xspConfigsLocation}

%install
install -d -m 755 $RPM_BUILD_ROOT%{appLocation}  
install -d -m 755 $RPM_BUILD_ROOT%{appDataLocation}  
install -d -m 755 $RPM_BUILD_ROOT%{xspAvailableApps}  
install -d -m 755 $RPM_BUILD_ROOT%{xspEnabledApps}  
  
cp -rap ./* $RPM_BUILD_ROOT%{appLocation}/  
  
echo "<web-application>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  
echo "  <name>%{appInstanceName}</name>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  
echo "  <vpath>%{appVirtualPath}</vpath>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  
echo "  <path>%{appLocation}/WeblogX</path>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  
echo "  <enabled>true</enabled>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  
echo "</web-application>" >> $RPM_BUILD_ROOT%{xspAvailableApps}/%{name}.webapp  

