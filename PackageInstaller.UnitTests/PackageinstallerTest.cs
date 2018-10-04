using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PackageInstaller;
using PackageInstaller.Packages;
using PackageInstaller = PackageInstaller.Packages.PackageInstaller;

namespace PackageInstaller.UnitTests
{
    [TestFixture]
    class PackageinstallerTest
    {
        private Packages.PackageInstaller _packageInstaller;
        private List<Package> _packages;
        private string[] _dependancyList;

        [Test]
        public void AddPackages()
        {
            const int two = 2;
            List<Package> _packagesTestList = new List<Package>()
            {
                new Package("KittenService", "CamelCaser"),
                new Package("CamelCaser", "")
            };

            //instantiate string array
            _dependancyList = new[]
                        {
                    "KittenService: CamelCaser",
                    "CamelCaser: "
                };

            //instantiate Packcage class
            _packageInstaller = new Packages.PackageInstaller();


            //Add Packages
            _packages = _packageInstaller.AddPackagesToList(_dependancyList);

            Assert.That(_packages, Is.Not.Null);
            Assert.That(_packagesTestList, Is.Not.Null);
            Assert.That(_packages.Count, Is.EqualTo(two));
            Assert.That(_packages[0].DependencyName, Is.EqualTo(_packagesTestList[0].DependencyName));
            Assert.That(_packages[1].DependencyName, Is.EqualTo(_packagesTestList[1].DependencyName));
            Assert.That(_packages[0].PackageName, Is.EqualTo(_packagesTestList[0].PackageName));
            Assert.That(_packages[1].PackageName, Is.EqualTo(_packagesTestList[1].PackageName));

        }


    }
}
