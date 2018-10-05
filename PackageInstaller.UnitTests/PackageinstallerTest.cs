using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private string[] _dependencyList;
        private string[] __dependencyInvalidList;
        private string[] _correctlySortedList;
        private string _correctlyStringOutput;

        [Test]
        public void AddPackages()
        {
            const int two = 2;
            List<Package> _packagesTestList = new List<Package>()
            {
                new Package("KittenService", "CamelCaser"),
                new Package("CamelCaser","")
            };

            //instantiate string array
            _dependencyList = new[]
            {
                "KittenService: CamelCaser",
                "CamelCaser: "
            };

            //instantiate Packcage class
            _packageInstaller = new Packages.PackageInstaller();


            //Add Packages
            _packages = _packageInstaller.AddPackagesToList(_dependencyList);

            Assert.That(_packages, Is.Not.Null);
            Assert.That(_packagesTestList, Is.Not.Null);
            Assert.That(_packages.Count, Is.EqualTo(two));
            Assert.That(_packages[0].DependencyName, Is.EqualTo(_packagesTestList[0].DependencyName));
            Assert.That(_packages[1].DependencyName, Is.EqualTo(_packagesTestList[1].DependencyName));
            Assert.That(_packages[0].PackageName, Is.EqualTo(_packagesTestList[0].PackageName));
            Assert.That(_packages[1].PackageName, Is.EqualTo(_packagesTestList[1].PackageName));

        }

        [Test]
        public void PackagesAndTheirDependencies()
        {

            //instantiate string array
            _dependencyList = new[]
            {
                "KittenService:",
                "Leetmeme: Cyberportal",
                "Cyberportal: Ice",
                "CamelCaser: KittenService",
                "Fraudstream: Leetmeme",
                "Ice: "
            };

            _correctlySortedList = new[]
            {
                "KittenService",
                "Ice",
                "Cyberportal",
                "Leetmeme",
                "CamelCaser",
                "Fraudstream"
            };

            _correctlyStringOutput = "KittenService, Ice, Cyberportal, Leetmeme, CamelCaser, Fraudstream";

            //instantiate Packcage class
            _packageInstaller = new Packages.PackageInstaller();

            //Add Resolve Dependecies
            var sortedOutput = _packageInstaller.OutputPackageInstaller(_dependencyList);
            var result = String.Join(", ", sortedOutput.ToArray());


            Assert.That(sortedOutput, Is.Not.Null);
            Assert.That(sortedOutput.FirstOrDefault(), Is.EqualTo(_correctlySortedList[0]));
            Assert.That(_correctlyStringOutput.Length, Is.EqualTo(result.Length));

        }

        [Test]
        public void PackagesAndTheirCycles()
        {

            //instantiate string array
            __dependencyInvalidList = new[]
            {
                "KittenService: ",
                "Leetmeme: Cyberportal",
                "Cyberportal: Ice",
                "CamelCaser: KittenService",
                "Fraudstream: ",
                "Ice: Leetmeme"
            };

            //instantiate Packcage class
            _packageInstaller = new Packages.PackageInstaller();

            //Add Resolve Dependecies
            var sortedOutput =_packageInstaller.OutputPackageInstaller(_dependencyList);
            var result = String.Join(", ", sortedOutput.ToArray());

            Assert.That(result, Is.EqualTo("error parsing packages: Invalid Input"));

        }
    }
}
