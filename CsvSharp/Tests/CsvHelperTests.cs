using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CsvSharp.Tests
{
    public class CsvHelperTests
    {
        [Fact]
        public async Task ReadCsv_ShouldReturnContent_WhenCalledWithValidPath()
        {
            //Arrange
            var mockCsvReader=new Mock<ICsvReader>();
            var expectedContent = "field1,field2,field3\nvalue1,value2,value3";
            mockCsvReader.Setup(reader=>reader.Read(It.IsAny<string>(),It.IsAny<CsvReaderType>()))
                        .ReturnsAsync(expectedContent);
            var csvHelper=new CsvHelper(mockCsvReader.Object);
            var path = @"C:\Users\anilk\Downloads\testCsv.csv";
            var type=CsvReaderType.FileSystem;

            //Act
            var result=await csvHelper.ReadCsv(path, type);

            //Assert
            Assert.Equal(expectedContent, result);
        }
    }
}
