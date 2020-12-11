﻿using System.Collections.Generic;
using Amazon.Pay.API.InStore.Charge;
using Amazon.Pay.API.InStore.Interfaces;
using Amazon.Pay.API.InStore.MerchantScan;
using Amazon.Pay.API.InStore.Refund;
using Moq;
using NUnit.Framework;

namespace Amazon.Pay.API.SDK.Tests.InStore.Interfaces
{
    [TestFixture]
    public class IInStoreClientTest
    {
        private Mock<IInStoreClient> mockInStoreClient;

        [OneTimeSetUp]
        public void Init()
        {
            this.mockInStoreClient = new Mock<IInStoreClient>(MockBehavior.Strict);
        }

        [SetUp]
        public void Setup()
        {
            this.mockInStoreClient.Reset();
        }

        [Test]
        public void MerchantScanCanBeMock()
        {
            var response = new MerchantScanResponse();
            this.mockInStoreClient.Setup(misc => misc.MerchantScan(It.IsAny<MerchantScanRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(response);

            var result = this.mockInStoreClient.Object.MerchantScan(new MerchantScanRequest("ScanData", "ScanReferenceId", Types.Currency.USD, "MerchantCOE"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(response));
            this.mockInStoreClient.Verify(misc => misc.MerchantScan(It.IsAny<MerchantScanRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void ChargeCanBeMocked()
        {
            var chargeResponse = new ChargeResponse();
            this.mockInStoreClient.Setup(misc => misc.Charge(It.IsAny<CreateChargeRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(chargeResponse);

            var result = this.mockInStoreClient.Object.Charge(new CreateChargeRequest("ChargePermissionId", 100, Types.Currency.USD, "chargeRefernceId"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(chargeResponse));
            this.mockInStoreClient.Verify(misc => misc.Charge(It.IsAny<CreateChargeRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }

        [Test]
        public void RefundCanBeMocked()
        {
            var refundResponse = new RefundResponse();
            this.mockInStoreClient.Setup(mwsc => mwsc.Refund(It.IsAny<CreateRefundRequest>(), It.IsAny<Dictionary<string, string>>())).Returns(refundResponse);

            var result = this.mockInStoreClient.Object.Refund(new CreateRefundRequest("ChargeId", 10, Types.Currency.USD, "RefundReferenceId"), new Dictionary<string, string>());

            Assert.That(result, Is.EqualTo(refundResponse));
            this.mockInStoreClient.Verify(mwsc => mwsc.Refund(It.IsAny<CreateRefundRequest>(), It.IsAny<Dictionary<string, string>>()), Times.Once);
        }
    }
}
