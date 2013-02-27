using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleGildedRose;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleGildedRoseTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestUpdateQualityDecreaseSellinAndQualityValueOf1()
        {
            //actor
            IList<Item> itemsOrigin = GetListWithoutSpecialItems();
            Inn inn = new Inn()
                {
                    Items = GetListWithoutSpecialItems()
                };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin.First(it => itemCourant.Name.Equals(it.Name));
                Assert.AreEqual(itemOrigin.Quality - 1, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
            }
        }

        [TestMethod]
        public void TestUpdateQualityDegradesTwiceAsFastWhenDatePassed()
        {
            //actor
            IList<Item> itemsOrigin = GetListItemsSellDatePassed();
            Inn inn = new Inn()
            {
                Items = GetListItemsSellDatePassed()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin.First(it => itemCourant.Name.Equals(it.Name));
                Assert.AreEqual(itemOrigin.Quality - 2, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
            }
        }

        [TestMethod]
        public void TestUpdateQualityNeverNegative()
        {
            const int requiredQuality = 0;

            //actor
            IList<Item> itemsOrigin = GetFullListItemsWithinputQuality(requiredQuality);
            Inn inn = new Inn()
            {
                Items = GetFullListItemsWithinputQuality(requiredQuality)
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin.First(it => itemCourant.Name.Equals(it.Name));
                Assert.IsTrue(itemOrigin.Quality >= 0);
            }
        }

        [TestMethod]
        public void TestUpdateQualityOfAgedBrieImproveWhenOlder()
        {
            const int inputQuality = 0;
            //actor
            IList<Item> itemsOrigin = GetOneAgedBrieWithQuality(inputQuality);
            Inn inn = new Inn()
            {
                Items = GetOneAgedBrieWithQuality(inputQuality)
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin.First(it => itemCourant.Name.Equals(it.Name));
                Assert.AreEqual(itemOrigin.Quality + 1, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
            }
        }

        [TestMethod]
        public void TestUpdateQualityIncreaseNeverMoreThan50()
        {
            const int requiredQuality = 50;

            //actor
            IList<Item> itemsOrigin = GetOneAgedBrieWithQuality(requiredQuality);
            Inn inn = new Inn()
            {
                Items = GetOneAgedBrieWithQuality(requiredQuality)
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin.First(it => itemCourant.Name.Equals(it.Name));
                Assert.AreEqual(requiredQuality, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
            }
        }

        [TestMethod]
        public void TestUpdateQualitySulfurasLegendary()
        {
            //actor
            IList<Item> itemsOrigin = GetSulfuras();
            Inn inn = new Inn()
            {
                Items = GetSulfuras()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            int numItem=0;
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin[numItem];
                Assert.AreEqual(itemOrigin.Quality, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn, itemCourant.SellIn);
                numItem++;
            }
        }

        [TestMethod]
        public void TestUpdateQualityBackstageIncreaseQualityBy2When10DaysLeftAndMoreThan5DaysAndSellInPositive()
        {
            //actor
            IList<Item> itemsOrigin = GetBackstageItems10DaysAndMoreThan5DaysLeftAndSellInPositive();
            Inn inn = new Inn()
            {
                Items = GetBackstageItems10DaysAndMoreThan5DaysLeftAndSellInPositive()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            int numItem = 0;
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin[numItem];
                Assert.AreEqual(itemOrigin.Quality+2, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn-1, itemCourant.SellIn);
                numItem++;
            }
        }

        [TestMethod]
        public void TestUpdateQualityBackstageIncreaseQualityBy3When5DaysLeftOrLessAndSellInPositive()
        {
            //actor
            IList<Item> itemsOrigin = GetBackstageIncreaseQualityBy3When5DaysLeftOrLessAndSellInPositive();
            Inn inn = new Inn()
            {
                Items = GetBackstageIncreaseQualityBy3When5DaysLeftOrLessAndSellInPositive()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            int numItem = 0;
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin[numItem];
                Assert.AreEqual(itemOrigin.Quality + 3, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
                numItem++;
            }
        }

        [TestMethod]
        public void TestUpdateQualityBackstageDropTo0AfterConcert()
        {
            //actor
            IList<Item> itemsOrigin = GetBackstageWithSellIn0();
            Inn inn = new Inn()
            {
                Items = GetBackstageWithSellIn0()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            int numItem = 0;
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin[numItem];
                Assert.AreEqual(0, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
                numItem++;
            }
        }

        [TestMethod]
        public void TestUpdateQualityConjuredWithQualityTwiceAsFast()
        {
            //actor
            IList<Item> itemsOrigin = GetConjuredWithQualityTwiceAsFast();
            Inn inn = new Inn()
            {
                Items = GetConjuredWithQualityTwiceAsFast()
            };

            //action
            inn.UpdateQualityAndSellInAfterADay();

            //assert
            int numItem = 0;
            foreach (Item itemCourant in inn.Items)
            {
                Item itemOrigin = itemsOrigin[numItem];
                Assert.AreEqual(itemOrigin.Quality - 2, itemCourant.Quality);
                Assert.AreEqual(itemOrigin.SellIn - 1, itemCourant.SellIn);
                numItem++;
            }
        }

        private IList<Item> GetConjuredWithQualityTwiceAsFast()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    // this conjured item does not work properly yet
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                };

            return itemsOrigin;
        }

        private IList<Item> GetBackstageWithSellIn0()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 0,
                            Quality = 30
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 0,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 0,
                            Quality = 12
                        },
                };
            return itemsOrigin;
        }

        private IList<Item> GetBackstageIncreaseQualityBy3When5DaysLeftOrLessAndSellInPositive()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 1,
                            Quality = 30
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 3,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 5,
                            Quality = 12
                        },
                };

            return itemsOrigin;
        }

        private IList<Item> GetBackstageItems10DaysAndMoreThan5DaysLeftAndSellInPositive()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 10,
                            Quality = 30
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 7,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 6,
                            Quality = 12
                        },
                };

            return itemsOrigin;
        }

        private IList<Item> GetSulfuras()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                      new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                      new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}
                };

            return itemsOrigin;
        }

        private IList<Item> GetOneAgedBrieWithQuality(int inputQuality)
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = inputQuality},
                };

            return itemsOrigin;
        }

        private IList<Item> GetFullListItemsWithinputQuality(int inputQuality)
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = inputQuality},
                    new Item {Name = "Aged Brie", SellIn = 2, Quality = inputQuality},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = inputQuality},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = inputQuality},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = inputQuality},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 15,
                            Quality = inputQuality
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 10,
                            Quality = inputQuality
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 5,
                            Quality = inputQuality
                        },
                    // this conjured item does not work properly yet
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = inputQuality}
                };

            return itemsOrigin;
        }

        private IList<Item> GetListItemsSellDatePassed()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 0, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 0, Quality = 7},
                };
            return itemsOrigin;
        }

        private static IList<Item> GetListWithoutSpecialItems()
        {
            IList<Item> itemsOrigin = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                };
            return itemsOrigin;
        }
    }
}
