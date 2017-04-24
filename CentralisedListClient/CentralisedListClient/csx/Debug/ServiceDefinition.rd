<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CentralisedListClient" generation="1" functional="0" release="0" Id="2fbbbdad-2a0a-4463-ba2d-b315f05f88ff" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CentralisedListClientGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="CentralisedListClientRole:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CentralisedListClient/CentralisedListClientGroup/LB:CentralisedListClientRole:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="CentralisedListClientRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CentralisedListClient/CentralisedListClientGroup/MapCentralisedListClientRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="CentralisedListClientRoleInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CentralisedListClient/CentralisedListClientGroup/MapCentralisedListClientRoleInstances" />
          </maps>
        </aCS>
        <aCS name="ServiceBus.WorkerClient:Microsoft.ServiceBus.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CentralisedListClient/CentralisedListClientGroup/MapServiceBus.WorkerClient:Microsoft.ServiceBus.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ServiceBus.WorkerClient:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CentralisedListClient/CentralisedListClientGroup/MapServiceBus.WorkerClient:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="ServiceBus.WorkerClientInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CentralisedListClient/CentralisedListClientGroup/MapServiceBus.WorkerClientInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:CentralisedListClientRole:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRole/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCentralisedListClientRole:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRole/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCentralisedListClientRoleInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRoleInstances" />
          </setting>
        </map>
        <map name="MapServiceBus.WorkerClient:Microsoft.ServiceBus.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClient/Microsoft.ServiceBus.ConnectionString" />
          </setting>
        </map>
        <map name="MapServiceBus.WorkerClient:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClient/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapServiceBus.WorkerClientInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClientInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CentralisedListClientRole" generation="1" functional="0" release="0" software="C:\Projects\CentralisedList\CentralisedListClient\CentralisedListClient\csx\Debug\roles\CentralisedListClientRole" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CentralisedListClientRole&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CentralisedListClientRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ServiceBus.WorkerClient&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRoleInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRoleUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRoleFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="ServiceBus.WorkerClient" generation="1" functional="0" release="0" software="C:\Projects\CentralisedList\CentralisedListClient\CentralisedListClient\csx\Debug\roles\ServiceBus.WorkerClient" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.ServiceBus.ConnectionString" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;ServiceBus.WorkerClient&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CentralisedListClientRole&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;ServiceBus.WorkerClient&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClientInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClientUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CentralisedListClient/CentralisedListClientGroup/ServiceBus.WorkerClientFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="CentralisedListClientRoleUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="ServiceBus.WorkerClientUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="CentralisedListClientRoleFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="ServiceBus.WorkerClientFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="CentralisedListClientRoleInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="ServiceBus.WorkerClientInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="7ba36c31-3964-449b-963a-21d07693b0d4" ref="Microsoft.RedDog.Contract\ServiceContract\CentralisedListClientContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="daf669d0-e379-491c-9c91-3f3ff2d6119e" ref="Microsoft.RedDog.Contract\Interface\CentralisedListClientRole:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CentralisedListClient/CentralisedListClientGroup/CentralisedListClientRole:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>