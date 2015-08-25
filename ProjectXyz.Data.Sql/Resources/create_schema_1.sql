CREATE TABLE [DisplayLanguages] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Name] VARCHAR(256) NOT NULL ON CONFLICT FAIL);


CREATE TABLE [DisplayStrings] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [LanguageId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_LanguageId] REFERENCES [DisplayLanguages]([Id]), 
  [Value] TEXT NOT NULL ON CONFLICT FAIL);


CREATE TABLE [Stats] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Name] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE, 
  [DisplayStringId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringId] REFERENCES [DisplayStrings]([Id]) COLLATE NOCASE);

---------------------------------------------------------------------------------------------------
-- ENCHANTMENTS
-- http://agiledata.org/essays/mappingObjects.html#MappingInheritance
---------------------------------------------------------------------------------------------------
CREATE TABLE [EnchantmentTypes] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [StoreRepositoryClassName] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [DefinitionRepositoryClassName] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [Enchantments] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  [TriggerId] GUID NOT NULL ON CONFLICT FAIL, 
  [StatusTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantments] (
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]), 
  [Expression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [RemainingDuration] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentValues] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ExpressionEnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [Value] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentStats] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ExpressionEnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]));

CREATE TABLE [OneShotNegateEnchantments] (
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]));

CREATE TABLE [EnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  [TriggerId] GUID NOT NULL ON CONFLICT FAIL, 
  [StatusTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]), 
  [Expression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [MinimumDuration] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumDuration] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentStatDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [ExpressionEnchantmentValueDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [MinimumValue] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumValue] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [OneShotNegateEnchantmentDefinitions] (
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]));

CREATE TABLE [EnchantmentStatuses] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [EnchantmentTriggers] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [StatusNegations] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]),
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]));

---------------------------------------------------------------------------------------------------
-- AFFIXES
---------------------------------------------------------------------------------------------------
CREATE TABLE [ItemAffixes] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(128) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [IsPrefix] BIT NOT NULL ON CONFLICT FAIL,
  [MinimumLevel] INT NOT NULL ON CONFLICT FAIL,
  [MaximumLevel] INT NOT NULL ON CONFLICT FAIL,
  [AffixMagicTypesId] BIT NOT NULL ON CONFLICT FAIL,
  [AffixEnchantmentsId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE AffixEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL);
  
CREATE TABLE AffixMagicTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE MagicTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE MagicTypesRandomAffixes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL,
  [MinimumAffixes] GUID NOT NULL ON CONFLICT FAIL,
  [MaximumAffixes] GUID NOT NULL ON CONFLICT FAIL);

---------------------------------------------------------------------------------------------------
-- DISEASES
---------------------------------------------------------------------------------------------------
CREATE TABLE Diseases (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [DiseaseStatesId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseStates (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [PreviousStateId] GUID NOT NULL ON CONFLICT FAIL, 
  [NextStateId] GUID NOT NULL ON CONFLICT FAIL, 
  [DiseaseStatesEnchantmentsId] GUID NOT NULL ON CONFLICT FAIL,
  [DiseaseSpreadTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseStatesEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [DiseaseStateId] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseSpreadTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);