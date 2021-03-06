﻿using System;
using System.IO;
using System.Reflection;
using ICities;
using UnityEngine;

namespace DifficultyOptions{

    public class DifficultyEconomy : EconomyExtensionBase
    {

        public override int OnGetConstructionCost(int originalConstructionCost, Service service, SubService subService, Level level) {
            return (int)(originalConstructionCost * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getConstructionCost() / 100.0f));
        }

        public override int OnGetMaintenanceCost(int originalMaintenanceCost, Service service, SubService subService, Level level) {
            return (int)(originalMaintenanceCost * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getMaintenanceCost() / 100.0f));
        }

        public override int OnGetRelocationCost(int constructionCost, int relocationCost, Service service, SubService subService, Level level) {
            return (int)(constructionCost * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getRelocationCost() / 100.0f));
        }

        public override int OnGetRefundAmount(int constructionCost, int refundAmount, Service service, SubService subService, Level level) {
            return (int)(constructionCost * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getRefundCost() / 100.0f));
        }

    }

    public class HardModeDemand : DemandExtensionBase {

        public override int OnCalculateResidentialDemand(int originalDemand) {
            return (int)((originalDemand + DifficultyOptionsMod.DIFFICULTY_SETTINGS.getResidentialFlat()) * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getResidentialPercentage() / 100.0f));
        }

        public override int OnCalculateCommercialDemand(int originalDemand) {
            return (int)((originalDemand + DifficultyOptionsMod.DIFFICULTY_SETTINGS.getCommercialFlat()) * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getCommercialPercentage() / 100.0f));
        }

        public override int OnCalculateWorkplaceDemand(int originalDemand) {
            return (int)((originalDemand + DifficultyOptionsMod.DIFFICULTY_SETTINGS.getIndustrialFlat()) * (DifficultyOptionsMod.DIFFICULTY_SETTINGS.getIndustrialPercentage() / 100.0f));
        }

    }
    /*
        public class HardModeLevelUp : LevelUpExtensionBase {

            public override ResidentialLevelUp OnCalculateResidentialLevelUp(ResidentialLevelUp levelUp, int averageEducation, int landValue, ushort buildingID, Service service, SubService subService, Level currentLevel) {
                if (levelUp.landValueProgress != 0) {
                    Level targetLevel;

                    if (landValue < 15) {
                        targetLevel = Level.Level1;
                        levelUp.landValueProgress = 1 + (landValue * 15 + 7) / 15;
                    } else if (landValue < 35) {
                        targetLevel = Level.Level2;
                        levelUp.landValueProgress = 1 + ((landValue - 15) * 15 + 10) / 20;
                    } else if (landValue < 60) {
                        targetLevel = Level.Level3;
                        levelUp.landValueProgress = 1 + ((landValue - 35) * 15 + 12) / 25;
                    } else if (landValue < 85) {
                        targetLevel = Level.Level4;
                        levelUp.landValueProgress = 1 + ((landValue - 60) * 15 + 12) / 25;
                    } else {
                        targetLevel = Level.Level5;
                        levelUp.landValueProgress = 1;
                    }

                    if (targetLevel < currentLevel) {
                        levelUp.landValueProgress = 1;
                    } else if (targetLevel > currentLevel) {
                        levelUp.landValueProgress = 15;
                    }

                    if (targetLevel < levelUp.targetLevel) {
                        levelUp.targetLevel = targetLevel;
                    }
                }

                levelUp.landValueTooLow = false;
                if (currentLevel == Level.Level2) {
                    if (landValue == 0) levelUp.landValueTooLow = true;
                } else if (currentLevel == Level.Level3) {
                    if (landValue < 21) levelUp.landValueTooLow = true;
                } else if (currentLevel == Level.Level4) {
                    if (landValue < 46) levelUp.landValueTooLow = true;
                } else if (currentLevel == Level.Level5) {
                    if (landValue < 71) levelUp.landValueTooLow = true;
                }

                return levelUp;
            }

            public override CommercialLevelUp OnCalculateCommercialLevelUp(CommercialLevelUp levelUp, int averageWealth, int landValue, ushort buildingID, Service service, SubService subService, Level currentLevel) {
                if (levelUp.landValueProgress != 0) {
                    Level targetLevel;

                    if (landValue < 30) {
                        targetLevel = Level.Level1;
                        levelUp.landValueProgress = 1 + (landValue * 15 + 15) / 30;
                    } else if (landValue < 60) {
                        targetLevel = Level.Level2;
                        levelUp.landValueProgress = 1 + ((landValue - 30) * 15 + 15) / 30;
                    } else {
                        targetLevel = Level.Level5;
                        levelUp.landValueProgress = 1;
                    }

                    if (targetLevel < currentLevel) {
                        levelUp.landValueProgress = 1;
                    } else if (targetLevel > currentLevel) {
                        levelUp.landValueProgress = 15;
                    }

                    if (targetLevel < levelUp.targetLevel) {
                        levelUp.targetLevel = targetLevel;
                    }
                }

                levelUp.landValueTooLow = false;
                if (currentLevel == Level.Level2) {
                    if (landValue < 15) levelUp.landValueTooLow = true;
                } else if (currentLevel == Level.Level3) {
                    if (landValue < 40) levelUp.landValueTooLow = true;
                }

                return levelUp;
            }

            public override IndustrialLevelUp OnCalculateIndustrialLevelUp(IndustrialLevelUp levelUp, int averageEducation, int serviceScore, ushort buildingID, Service service, SubService subService, Level currentLevel) {
                if (levelUp.serviceProgress != 0) {
                    Level targetLevel;

                    if (serviceScore < 40) {
                        targetLevel = Level.Level1;
                        levelUp.serviceProgress = 1 + (serviceScore * 15 + 20) / 40;
                    } else if (serviceScore < 80) {
                        targetLevel = Level.Level2;
                        levelUp.serviceProgress = 1 + ((serviceScore - 40) * 15 + 20) / 40;
                    } else {
                        targetLevel = Level.Level5;
                        levelUp.serviceProgress = 1;
                    }

                    if (targetLevel < currentLevel) {
                        levelUp.serviceProgress = 1;
                    } else if (targetLevel > currentLevel) {
                        levelUp.serviceProgress = 15;
                    }

                    if (targetLevel < levelUp.targetLevel) {
                        levelUp.targetLevel = targetLevel;
                    }
                }

                levelUp.tooFewServices = false;
                if (currentLevel == Level.Level2) {
                    if (serviceScore < 25) levelUp.tooFewServices = true;
                } else if (currentLevel == Level.Level3) {
                    if (serviceScore < 60) levelUp.tooFewServices = true;
                }

                return levelUp;
            }

            public override OfficeLevelUp OnCalculateOfficeLevelUp(OfficeLevelUp levelUp, int averageEducation, int serviceScore, ushort buildingID, Service service, SubService subService, Level currentLevel) {
                if (levelUp.serviceProgress != 0) {
                    Level targetLevel;

                    if (serviceScore < 55) {
                        targetLevel = Level.Level1;
                        levelUp.serviceProgress = 1 + (serviceScore * 15 + 27) / 55;
                    } else if (serviceScore < 110) {
                        targetLevel = Level.Level2;
                        levelUp.serviceProgress = 1 + ((serviceScore - 55) * 15 + 27) / 55;
                    } else {
                        targetLevel = Level.Level5;
                        levelUp.serviceProgress = 1;
                    }

                    if (targetLevel < currentLevel) {
                        levelUp.serviceProgress = 1;
                    } else if (targetLevel > currentLevel) {
                        levelUp.serviceProgress = 15;
                    }

                    if (targetLevel < levelUp.targetLevel) {
                        levelUp.targetLevel = targetLevel;
                    }
                }

                levelUp.tooFewServices = false;
                if (currentLevel == Level.Level2) {
                    if (serviceScore < 35) levelUp.tooFewServices = true;
                } else if (currentLevel == Level.Level3) {
                    if (serviceScore < 80) levelUp.tooFewServices = true;
                }

                return levelUp;
            }

        }*/

}
