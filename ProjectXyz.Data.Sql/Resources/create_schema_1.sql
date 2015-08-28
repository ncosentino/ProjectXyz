---------------------------------------------------------------------------------------------------
-- RESOURCES
---------------------------------------------------------------------------------------------------
CREATE TABLE [DisplayLanguages] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Name] VARCHAR(256) NOT NULL ON CONFLICT FAIL);

CREATE TABLE [DisplayStrings] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [LanguageId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_LanguageId] REFERENCES [DisplayLanguages]([Id]), 
  [Value] TEXT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [GraphicResources] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Value] TEXT NOT NULL ON CONFLICT FAIL);

---------------------------------------------------------------------------------------------------
-- STATS
---------------------------------------------------------------------------------------------------
CREATE TABLE [StatDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Name] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_NameStringResourceId] REFERENCES [DisplayStrings]([Id]));

CREATE TABLE [Stats] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatDefinitionId] REFERENCES [StatDefinitions]([Id]), 
  [Value] FLOAT NOT NULL ON CONFLICT FAIL);

---------------------------------------------------------------------------------------------------
-- MATERIALS
---------------------------------------------------------------------------------------------------
CREATE TABLE MaterialTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

 ---------------------------------------------------------------------------------------------------
-- RACES
---------------------------------------------------------------------------------------------------
CREATE TABLE RaceDefinitions (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

---------------------------------------------------------------------------------------------------
-- CLASSES
---------------------------------------------------------------------------------------------------
CREATE TABLE ClassDefinitions (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

---------------------------------------------------------------------------------------------------
-- ITEMS
---------------------------------------------------------------------------------------------------
CREATE TABLE ItemTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

CREATE TABLE MagicTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

CREATE TABLE EquipSlotTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

CREATE TABLE SocketTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]));

CREATE TABLE StatSocketTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [SocketTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_SocketTypeId] REFERENCES [SocketTypes]([Id]));

CREATE TABLE ItemTypeEquipSlotTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  [EquipSlotTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EquipSlotTypeId] REFERENCES [EquipSlotTypes]([Id]));
  
CREATE TABLE Items (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]),
  [InventoryGraphicResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_InventoryGraphicResourceId] REFERENCES [GraphicResources]([Id]),
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]),
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  [MaterialTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MaterialTypeId] REFERENCES [MaterialTypes]([Id]),
  [SocketTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_SocketTypeId] REFERENCES [SocketTypes]([Id]));

CREATE TABLE ItemStatRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]));

CREATE TABLE ItemMiscRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [RaceDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_RaceDefinitionId] REFERENCES [RaceDefinitions]([Id]),
  [ClassDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ClassDefinitionId] REFERENCES [ClassDefinitions]([Id]));

CREATE TABLE ItemStats (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]));

CREATE TABLE ItemEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]));

CREATE TABLE SocketedItems (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ParentItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ParentItemId] REFERENCES [Items]([Id]),
  [ChildItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ChildItemId] REFERENCES [Items]([Id])); 

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
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [ExpressionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ExpressionId] REFERENCES [ExpressionDefinitions]([Id]),
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
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Statss]([Id]));

CREATE TABLE [EnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  [TriggerId] GUID NOT NULL ON CONFLICT FAIL, 
  [StatusTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [Expression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [CalculationPriority] INTEGER NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [ExpressionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ExpressionId] REFERENCES [ExpressionDefinitions]([Id]),
  [MinimumDuration] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumDuration] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentStatDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [ExpressionEnchantmentValueDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [MinimumValue] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumValue] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [OneShotNegateEnchantmentDefinitions] (
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]));

CREATE TABLE [EnchantmentStatuses] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [EnchantmentTriggers] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [Name] VARCHAR(64) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE [StatusNegations] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]),
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]));

---------------------------------------------------------------------------------------------------
-- AFFIXES
---------------------------------------------------------------------------------------------------
CREATE TABLE [ItemAffixDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayStringResourceId] REFERENCES [DisplayStrings]([Id]),
  [IsPrefix] BIT NOT NULL ON CONFLICT FAIL,
  [MinimumLevel] INT NOT NULL ON CONFLICT FAIL,
  [MaximumLevel] INT NOT NULL ON CONFLICT FAIL);

CREATE TABLE ItemAffixMagicTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [ItemAffixDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemAffixId] REFERENCES [ItemAffixDefinitions]([Id]),
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]));

CREATE TABLE ItemAffixEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [ItemAffixDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemAffixId] REFERENCES [ItemAffixDefinitions]([Id]),
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]));

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