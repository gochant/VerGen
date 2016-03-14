<?xml version="1.0"?>
<BusinessModelContainer xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xsi:type="BusinessModelContainerViewModel">
  <EdmFilePath>Designer\ER.edmx</EdmFilePath>
  <SaveFilePath>C:\Users\QingGuo\Documents\Code\VerGen\TestConsole\Designer\BM.bmx</SaveFilePath>
  <Packages>
    <BusinessModelPackage xsi:type="BusinessModelPackageViewModel" Name="Position" Display="岗位 ">
      <CommonModel xsi:type="CommonModelDefineViewModel">
        <Fields>
          <ModelFieldDefine xsi:type="ModelFieldDefineViewModel" Name="Id" Type="string" IsCalculated="false" IsReadonly="false" IsKey="false">
            <CtoVMap IsUsedAsExpression="false" />
            <VtoCMap IsUsedAsExpression="false" />
            <Customs />
            <Display IsApplicable="true" Enabled="false" Name="Id" IsI18N="false" />
            <DisplayField IsApplicable="true" Enabled="false" />
            <DataType IsApplicable="true" Enabled="false" />
            <DisplayFormat IsApplicable="true" Enabled="false" />
            <UIHint IsApplicable="true" Enabled="false" />
            <Required IsApplicable="true" Enabled="true" AllowEmptyStrings="false" />
            <StringLength IsApplicable="true" Enabled="false" Min="0" Max="50" />
            <Range IsApplicable="true" Enabled="false" Type="string" />
            <RegExp IsApplicable="true" Enabled="false" />
          </ModelFieldDefine>
          <ModelFieldDefine xsi:type="ModelFieldDefineViewModel" Name="Type" Type="PositionType" IsCalculated="false" IsReadonly="false" IsKey="false">
            <CtoVMap IsUsedAsExpression="false" />
            <VtoCMap IsUsedAsExpression="false" />
            <Customs />
            <Display IsApplicable="true" Enabled="false" Name="Type" IsI18N="false" />
            <DisplayField IsApplicable="true" Enabled="false" />
            <DataType IsApplicable="true" Enabled="false" />
            <DisplayFormat IsApplicable="true" Enabled="false" />
            <UIHint IsApplicable="true" Enabled="false" />
            <Required IsApplicable="true" Enabled="true" AllowEmptyStrings="false" />
            <StringLength IsApplicable="false" Enabled="false" Min="0" Max="50" />
            <Range IsApplicable="true" Enabled="false" Type="PositionType" />
            <RegExp IsApplicable="true" Enabled="false" />
          </ModelFieldDefine>
          <ModelFieldDefine xsi:type="ModelFieldDefineViewModel" Name="Date" Type="System.DateTime" IsCalculated="false" IsReadonly="false" IsKey="false">
            <CtoVMap IsUsedAsExpression="false" />
            <VtoCMap IsUsedAsExpression="false" />
            <Customs />
            <Display IsApplicable="true" Enabled="false" Name="Date" IsI18N="false" />
            <DisplayField IsApplicable="true" Enabled="false" />
            <DataType IsApplicable="true" Enabled="false" />
            <DisplayFormat IsApplicable="true" Enabled="false" />
            <UIHint IsApplicable="true" Enabled="false" />
            <Required IsApplicable="true" Enabled="true" AllowEmptyStrings="false" />
            <StringLength IsApplicable="false" Enabled="false" Min="0" Max="50" />
            <Range IsApplicable="true" Enabled="false" Type="System.DateTime" />
            <RegExp IsApplicable="true" Enabled="false" />
          </ModelFieldDefine>
          <ModelFieldDefine xsi:type="ModelFieldDefineViewModel" Name="Enabled" Type="bool" IsCalculated="false" IsReadonly="false" IsKey="false">
            <CtoVMap IsUsedAsExpression="false" />
            <VtoCMap IsUsedAsExpression="false" />
            <Customs />
            <Display IsApplicable="true" Enabled="false" Name="Enabled" IsI18N="false" />
            <DisplayField IsApplicable="true" Enabled="false" />
            <DataType IsApplicable="true" Enabled="false" />
            <DisplayFormat IsApplicable="true" Enabled="false" />
            <UIHint IsApplicable="true" Enabled="false" />
            <Required IsApplicable="true" Enabled="true" AllowEmptyStrings="false" />
            <StringLength IsApplicable="false" Enabled="false" Min="0" Max="50" />
            <Range IsApplicable="true" Enabled="false" Type="bool" />
            <RegExp IsApplicable="true" Enabled="false" />
          </ModelFieldDefine>
        </Fields>
      </CommonModel>
      <ViewModels />
      <CrudUserStory xsi:type="CrudUserStoryViewModel" />
    </BusinessModelPackage>
  </Packages>
</BusinessModelContainer>