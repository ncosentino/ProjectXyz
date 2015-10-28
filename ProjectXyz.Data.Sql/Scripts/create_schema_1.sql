---------------------------------------------------------------------------------------------------
-- RESOURCES
---------------------------------------------------------------------------------------------------
CREATE TABLE [DisplayLanguages] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [Name] VARCHAR(256) NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [StringResources] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [DisplayLanguageId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayLanguageId] REFERENCES [DisplayLanguages]([Id]), 
  [Value] TEXT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [GraphicResources] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [DisplayLanguageId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_DisplayLanguageId] REFERENCES [DisplayLanguages]([Id]),  -- FIXME: I don't think we need this, but if we want GFX with text on it then... we might.
  [Value] TEXT NOT NULL ON CONFLICT FAIL, -- FIXME: this likely shouldn't be a string...
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- STATS
---------------------------------------------------------------------------------------------------
CREATE TABLE [StatDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_NameStringResourceId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [Stats] (
  [Id] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [UNIQUE_Id] UNIQUE ON CONFLICT FAIL, 
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatDefinitionId] REFERENCES [StatDefinitions]([Id]), 
  [Value] FLOAT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- MATERIALS
---------------------------------------------------------------------------------------------------
CREATE TABLE MaterialTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

 ---------------------------------------------------------------------------------------------------
-- RACES
---------------------------------------------------------------------------------------------------
CREATE TABLE RaceDefinitions (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- CLASSES
---------------------------------------------------------------------------------------------------
CREATE TABLE ClassDefinitions (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- ITEMS
---------------------------------------------------------------------------------------------------
CREATE TABLE DropEntries (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ParentDropId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ParentDropId] REFERENCES [Drops]([Id]),
  [MagicTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeGroupingId] REFERENCES [MagicTypeGroupings]([GroupingId]),
  [WeatherTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_WeatherTypeGroupingId] REFERENCES [WeatherTypeGroupings]([GroupingId]),
  [ItemDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemDefinitionId] REFERENCES [ItemDefinitions]([Id]),
  [Weighting] INTEGER NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE DropLinks (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ParentDropId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ParentDropId] REFERENCES [Drops]([Id]),
  [ChildDropId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ChildDropId] REFERENCES [Drops]([Id]),
  [Weighting] INTEGER NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE Drops (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [Minimum] INTEGER NOT NULL ON CONFLICT FAIL,
  [Maximum] INTEGER NOT NULL ON CONFLICT FAIL,
  [CanRepeat] BIT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE ItemTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [ItemTypeGeneratorPlugins] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]),
  [ItemGeneratorClassName] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE);

CREATE TABLE MagicTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [RarityWeighting] INTEGER NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

 CREATE TABLE MagicTypeGroupings (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [GroupingId] GUID NOT NULL ON CONFLICT FAIL,
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE EquipSlotTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE SocketTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE StatSocketTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [SocketTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_SocketTypeId] REFERENCES [SocketTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemTypeEquipSlotTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  [EquipSlotTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EquipSlotTypeId] REFERENCES [EquipSlotTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemDefinitions (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [InventoryGraphicResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_InventoryGraphicResourceId] REFERENCES [GraphicResources]([Id]),
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]),
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  [MaterialTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MaterialTypeId] REFERENCES [MaterialTypes]([Id]),
  [SocketTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_SocketTypeId] REFERENCES [SocketTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemDefinitionStats (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemDefinitionId] REFERENCES [ItemDefinitions]([Id]),
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [MinimumValue] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumValue] FLOAT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE ItemDefinitionItemMiscRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemDefinitionId] REFERENCES [ItemDefinitions]([Id]),
  [ItemMiscRequirementsId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemMiscRequirementsId] REFERENCES [ItemMiscRequirements]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemDefinitionStatRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemDefinitionId] REFERENCES [ItemDefinitions]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]),
  PRIMARY KEY (Id));
  
CREATE TABLE Items (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemDefinitionId] REFERENCES [ItemDefinitions]([Id]),
  [ItemNamePartId] GUID NOT NULL ON CONFLICT FAIL,
  [InventoryGraphicResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_InventoryGraphicResourceId] REFERENCES [GraphicResources]([Id]),
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MagicTypeId] REFERENCES [MagicTypes]([Id]),
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  [MaterialTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MaterialTypeId] REFERENCES [MaterialTypes]([Id]),
  [SocketTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_SocketTypeId] REFERENCES [SocketTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemNameParts (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [PartId] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [Order] INT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE ItemStatRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemStoreItemMiscRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [ItemMiscRequirementsId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemMiscRequirementsId] REFERENCES [ItemMiscRequirements]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemMiscRequirements (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [RaceDefinitionId] GUID NULL ON CONFLICT FAIL CONSTRAINT [FK_RaceDefinitionId] REFERENCES [RaceDefinitions]([Id]),
  [ClassDefinitionId] GUID NULL ON CONFLICT FAIL CONSTRAINT [FK_ClassDefinitionId] REFERENCES [ClassDefinitions]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemStats (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE ItemEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemId] REFERENCES [Items]([Id]),
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE SocketedItems (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ParentItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ParentItemId] REFERENCES [Items]([Id]),
  [ChildItemId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ChildItemId] REFERENCES [Items]([Id]),
  PRIMARY KEY (Id)); 

CREATE TABLE ItemTypeGroupings (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [GroupingId] GUID NOT NULL ON CONFLICT FAIL,
  [ItemTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemTypeId] REFERENCES [ItemTypes]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE RandomItemNameAffixes (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [IsPrefix] BIT NOT NULL ON CONFLICT FAIL,
  [ItemTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL,
  [MagicTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL,
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));
---------------------------------------------------------------------------------------------------
-- ENCHANTMENTS
-- http://agiledata.org/essays/mappingObjects.html#MappingInheritance
---------------------------------------------------------------------------------------------------
CREATE TABLE [EnchantmentTypes] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [StoreRepositoryClassName] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [DefinitionRepositoryClassName] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  PRIMARY KEY (Id));

CREATE TABLE [Enchantments] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  [TriggerId] GUID NOT NULL ON CONFLICT FAIL, 
  [StatusTypeId] GUID NOT NULL ON CONFLICT FAIL,
  [WeatherTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_WeatherTypeGroupingId] REFERENCES [WeatherTypeGroupings]([GroupingId]),
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionEnchantments] (
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [ExpressionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ExpressionId] REFERENCES [ExpressionDefinitions]([Id]),
  [RemainingDuration] FLOAT NOT NULL ON CONFLICT FAIL);

CREATE TABLE [ExpressionEnchantmentValues] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ExpressionEnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [Value] FLOAT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionEnchantmentStats] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [ExpressionEnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Stats]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [OneShotNegateEnchantments] (
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentId] REFERENCES [Enchantments]([Id]),
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [Statss]([Id]));

CREATE TABLE [EnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  [TriggerId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTriggerId] REFERENCES [EnchantmentTriggers]([Id]), 
  [StatusTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentStatusId] REFERENCES [EnchantmentStatuses]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [Expression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [CalculationPriority] INTEGER NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionEnchantmentDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [ExpressionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ExpressionId] REFERENCES [ExpressionDefinitions]([Id]),
  [MinimumDuration] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumDuration] FLOAT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionEnchantmentStatDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  PRIMARY KEY (Id));

CREATE TABLE [ExpressionEnchantmentValueDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [IdForExpression] VARCHAR(256) NOT NULL ON CONFLICT FAIL COLLATE NOCASE,
  [MinimumValue] FLOAT NOT NULL ON CONFLICT FAIL, 
  [MaximumValue] FLOAT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [OneShotNegateEnchantmentDefinitions] (
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]), 
  [StatId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatId] REFERENCES [StatDefinitions]([Id]));

CREATE TABLE [EnchantmentStatuses] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [EnchantmentTriggers] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE [EnchantmentDefinitionWeatherGroupings] (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentDefinitionId]  GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]),
  [WeatherTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id));

CREATE TABLE [StatusNegations] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [StatDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StatDefinitionId] REFERENCES [StatDefinitions]([Id]),
  [EnchantmentTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentTypeId] REFERENCES [EnchantmentTypes]([Id]),
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- AFFIXES
---------------------------------------------------------------------------------------------------
CREATE TABLE [ItemAffixDefinitions] (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [IsPrefix] BIT NOT NULL ON CONFLICT FAIL,
  [MinimumLevel] INT NOT NULL ON CONFLICT FAIL,
  [MaximumLevel] INT NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id)); 

CREATE TABLE ItemAffixDefinitionMagicTypeGrouping (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [ItemAffixDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemAffixId] REFERENCES [ItemAffixDefinitions]([Id]),
  [MagicTypeGroupingId] GUID NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id)); 

CREATE TABLE ItemAffixDefinitionEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [ItemAffixDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_ItemAffixId] REFERENCES [ItemAffixDefinitions]([Id]),
  [EnchantmentDefinitionId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_EnchantmentDefinitionId] REFERENCES [EnchantmentDefinitions]([Id]),
  PRIMARY KEY (Id)); 

CREATE TABLE MagicTypesRandomAffixes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [MagicTypeId] GUID NOT NULL ON CONFLICT FAIL,
  [MinimumAffixes] GUID NOT NULL ON CONFLICT FAIL,
  [MaximumAffixes] GUID NOT NULL ON CONFLICT FAIL,
  PRIMARY KEY (Id)); 

---------------------------------------------------------------------------------------------------
-- WEATHER
---------------------------------------------------------------------------------------------------
CREATE TABLE WeatherTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  PRIMARY KEY (Id));

CREATE TABLE WeatherTypeGroupings (
  [Id] GUID NOT NULL ON CONFLICT FAIL,
  [GroupingId] GUID NOT NULL ON CONFLICT FAIL,
  [WeatherTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_WeatherTypeId] REFERENCES [WeatherTypes]([Id]),
  PRIMARY KEY (Id));

---------------------------------------------------------------------------------------------------
-- DISEASES
---------------------------------------------------------------------------------------------------
CREATE TABLE Diseases (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [DiseaseStatesId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseStates (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]),
  [PreviousStateId] GUID NOT NULL ON CONFLICT FAIL, 
  [NextStateId] GUID NOT NULL ON CONFLICT FAIL, 
  [DiseaseSpreadTypeId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseStatesEnchantments (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [DiseaseStateId] GUID NOT NULL ON CONFLICT FAIL,
  [EnchantmentId] GUID NOT NULL ON CONFLICT FAIL);

CREATE TABLE DiseaseSpreadTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]));

---------------------------------------------------------------------------------------------------
-- MAPS
---------------------------------------------------------------------------------------------------
CREATE TABLE TerrainTypes (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]));

CREATE TABLE MapTiles (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [MapId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_MapId] REFERENCES [Maps]([Id]),
  [TileGraphicResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_TileGraphicResourceId] REFERENCES [GraphicResources]([Id]),
  [TerrainTypeId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_TerrainTypeId] REFERENCES [TerrainTypes]([Id]),
  [X] INT NOT NULL ON CONFLICT FAIL,
  [Y] INT NOT NULL ON CONFLICT FAIL,
  [Z] INT NOT NULL ON CONFLICT FAIL);

CREATE TABLE Maps (
  [Id] GUID NOT NULL ON CONFLICT FAIL, 
  [NameStringResourceId] GUID NOT NULL ON CONFLICT FAIL CONSTRAINT [FK_StringResourcesId] REFERENCES [StringResources]([Id]));