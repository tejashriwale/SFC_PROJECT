using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.Model;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Model.Model;
using Project.Repository;
using System.Drawing;
using System.Net.Sockets;
using System.Numerics;

namespace Project.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<ApiResponseMessage, ApiResponseMessageDto>().ReverseMap();

            CreateMap<ApiResponseMessage2, ApiResponseMessage2Dto>().ReverseMap();

            CreateMap<ApiResponseMessage3, ApiResponseMessage3Dto>().ReverseMap();

            CreateMap<_GET_AccessRights_, _GET_AccessRights_Dto>().ReverseMap();

            CreateMap<_GET_BarcodeSettings_, _GET_BarcodeSettings_DTO>().ReverseMap();

            CreateMap<GET_BOMPacking, GET_BOMPackingDTO>().ReverseMap();

            CreateMap<_GET_CounterMaster_, _GET_CounterMaster_DTO>().ReverseMap();

            CreateMap<_GET_CustomersPartners_, GET_CustomersPartnersDto>().ReverseMap();

            CreateMap<_GET_DepartmentMaster_, _GET_DepartmentMaster_DTO>().ReverseMap();

            CreateMap<_GET_HSNMasters_, _GET_HSNMasters_DTO>().ReverseMap();

            CreateMap<_GET_PackingMasters_, _GET_PackingMasters_DTO>().ReverseMap();

            CreateMap<_GET_ProductMaster_, _GET_ProductMaster_DTO>().ReverseMap();

            CreateMap<_GET_RackMaster_, _GET_RackMaster_DTO>().ReverseMap();

            CreateMap<_GET_ReturnPolicy_, _GET_ReturnPolicy_DTOs>();

            CreateMap<_GET_ServicesMaster_, _GET_ServicesMaster_Dto>().ReverseMap();

            CreateMap<_GET_StateCodeMasters_, _GET_StateCodeMasters_DTO>().ReverseMap();

            CreateMap<_GET_UnitConversionMasters_, _GET_UnitConversionMasters_DTO>();

            CreateMap<Implications, ImplicationsDTO>().ReverseMap();

            CreateMap<_GET_UserMasters_, _GET_UserMasters_DTO>();

            CreateMap<_GET_UserProfile_, _GET_UserProfile_DTO>().ReverseMap();

            CreateMap<_GET_WarehouseMaster_, _GET_WarehouseMaster_DTO>().ReverseMap();

            CreateMap<WAREHOUSENAME, WAREHOUSENAMEDto>().ReverseMap();

            CreateMap<HSNGSTDetails, HSNGSTDetailsDTO>().ReverseMap();

            CreateMap<GET_HSNCode, GET_HSNCodeDTO>().ReverseMap();

            CreateMap<GET_UserProfileSearchDomain, GET_UserProfileSearchDto>().ReverseMap();

            CreateMap<StatePincode, StatePincodeDTO>().ReverseMap();

            CreateMap<GET_Category, GET_CategoryDTO>().ReverseMap();

            CreateMap<GET_CategoryCustomer, GET_CategoryCustomerDTO>().ReverseMap();

            CreateMap<GET_CategoryProduct, GET_CategoryProductDTO>().ReverseMap();

            CreateMap<GET_StateCodeDistinct, GET_StateCodeDistinctDto>().ReverseMap();

            CreateMap<GET_PRequisitionMaster, GET_PRequisitionMasterDTO>().ReverseMap();

            CreateMap<GET_PRequisitionDetails, GET_PRequisitionDetailsDto>().ReverseMap();

            CreateMap<GET_CustomerDetails, GET_CustomerDetailsDTO>().ReverseMap();

            CreateMap<GET_QuatationMaster, GET_QuatationMasterDTO>().ReverseMap();

            CreateMap<GET_PRequisitionNosearch, GET_PRequisitionNosearchDTO>().ReverseMap();

            CreateMap<GET_PRequisitionNumberSearch, GET_PRequisitionNumberSearchDTO>().ReverseMap();

            CreateMap<GET_Analysis, GET_AnalysisDTO>().ReverseMap();

            CreateMap<GET_AnalyzeData, GET_AnalyzeDataDto>().ReverseMap();

            CreateMap<GET_QuatationMasterSearch, GET_QuatationMasterSearchDto>().ReverseMap();

            CreateMap<GET_POrderMaster, GET_POrderMasterDto>().ReverseMap();

            CreateMap<GET_PurchaseOrderDataById, GET_PurchaseOrderDataByIdDto>().ReverseMap();

            CreateMap<GET_QuatationSearchNo, GET_QuatationSearchNoDto>().ReverseMap();

            CreateMap<GET_CustomerDetailsByID, GET_CustomerDetailsByIDDto>().ReverseMap();

            CreateMap<GET_PChallanMaster, GET_PChallanMasterDto>().ReverseMap();

            CreateMap<GET_SBillDetails, GET_SBillDetailsDto>().ReverseMap();

            CreateMap<GET_SBillMaster, GET_SBillMasterDto>().ReverseMap();

            CreateMap<GET_SChallanDetails, GET_SChallanDetailsDto>().ReverseMap();

            CreateMap<GET_SChallanMaster, GET_SChallanMasterDto>();

            CreateMap<GET_PChallanSearchByID, GET_PChallanSearchByIDDto>().ReverseMap();

            CreateMap<GET_SOrderDetails, GET_SOrderDetailsDto>().ReverseMap();

            CreateMap<GET_SOrderMaster, GET_SOrderMasterDto>().ReverseMap();

            CreateMap<GET_SRequisitionDetails, GET_SRequisitionDetailsDto>().ReverseMap();

            CreateMap<GET_GoodReceiptNoteMaster, GET_GoodReceiptNoteMasterDto>().ReverseMap();

            CreateMap<GET_SRequisitionMaster, GET_SRequisitionMasterDto>().ReverseMap();

            CreateMap<GET_PChallanNoSearch, GET_PChallanNoSearchDto>().ReverseMap();

            CreateMap<GET_SQuatationDetails, GET_SQuatationDetailsDto>().ReverseMap();

            CreateMap<GET_SQuatationMaster, GET_SQuatationMasterDto>().ReverseMap();

            CreateMap<GET_SReturnDetails, GET_SReturnDetailsDto>().ReverseMap();

            CreateMap<GET_SReturnMaster, GET_SReturnMasterDto>().ReverseMap();

            CreateMap<GET_PBillMaster, GET_PBillMasterDto>().ReverseMap();

            CreateMap<GET_PBillNoSearch, GET_PBillNoSearchDto>().ReverseMap();

            CreateMap<GET_POrderNoSearch, GET_POrderNoSearchDto>().ReverseMap();

            CreateMap<GET_PReturnMaster, GET_PReturnMasterDto>().ReverseMap();

            CreateMap<GET_SBillNoSearch, GET_SBillNoSearchDto>().ReverseMap();

            CreateMap<GET_SChallanNoSearch, GET_SChallanNoSearchDto>().ReverseMap();

            CreateMap<GET_SQuatationNoSearch, GET_SQuatationNoSearchDto>().ReverseMap();

            CreateMap<GET_SOrderNoSearch, GET_SOrderNoSearchDto>().ReverseMap();

            CreateMap<GET_PRequisitionDetailData, GET_PRequisitionDetailDataDto>().ReverseMap();

            CreateMap<GET_QuatationDetailData, GET_QuatationDetailDataDto>().ReverseMap();

            CreateMap<GET_SRequisitionNoSearch, GET_SRequisitionNoSearchDto>().ReverseMap();

            CreateMap<GET_QuatationSearchByID, GET_QuatationSearchByIDDto>().ReverseMap();

            CreateMap<GET_POrderDetailsData, GET_POrderDetailsDataDto>().ReverseMap();

            CreateMap<GET_BarCode, GET_BarCodeDto>().ReverseMap();

            CreateMap<GET_PChallanDetailsById, GET_PChallanDetailsByIdDto>().ReverseMap();

            CreateMap<GET_SBillDetailsById, GET_SBillDetailsByIdDto>().ReverseMap();

            CreateMap<GET_SChallanDetailsById, GET_SChallanDetailsByIdDto>().ReverseMap();

            CreateMap<GET_SRequisitionDetailById, GET_SRequisitionDetailByIdDto>().ReverseMap();

            CreateMap<GET_SReturnDetailsById, GET_SReturnDetailsByIdDto>().ReverseMap();

            CreateMap<GET_PBillDetailsData, GET_PBillDetailsDataDto>().ReverseMap();

            CreateMap<GET_HRM_Empoye_Leaves, GET_HRM_Empoye_LeavesDto>().ReverseMap();

            CreateMap<GET_HRM_Holiday, GET_HRM_HolidayDto>().ReverseMap();

            CreateMap<GET_SQuotationDetailById, GET_SQuotationDetailByIdDto>().ReverseMap();

            CreateMap<GET_GoodReceiptNoteDataById, GET_GoodReceiptNoteDataByIdDto>().ReverseMap();

            CreateMap<GET_HRMDesignationName, GET_HRMDesignationNameDto>().ReverseMap();

            CreateMap<GET_GoodReceiptNoteAssuranceById, GET_GoodReceiptNoteAssuranceByIdDto>().ReverseMap();

            CreateMap<GET_HRMShiftInfoName, GET_HRMShiftInfoNameDto>().ReverseMap();

            CreateMap<GET_CompanyMaster, GET_CompanyMasterDto>().ReverseMap();

            CreateMap<GET_LeaveType, GET_LeaveTypeDto>().ReverseMap();

            CreateMap<GET_DepartmentName, GET_DepartmentNameDto>().ReverseMap();

            CreateMap<GET_PReturnDataByID, GET_PReturnDataByIDDto>().ReverseMap();

            CreateMap<GET_HRMDesignation, GET_HRMDesignationDto>().ReverseMap();

            CreateMap<GET_HRMShiftInfo, GET_HRMShiftInfoDto>().ReverseMap();

            CreateMap<GET_PayStructure, GET_PayStructureDto>().ReverseMap();

            CreateMap<GET_SQuatationDataByID, GET_SQuatationDataByIDDto>().ReverseMap();

            CreateMap<GET_SRequisitionDataByID, GET_SRequisitionDataByIDDto>().ReverseMap();

            CreateMap<GET_HRMPayroll, GET_HRMPayrollDto>().ReverseMap();

            CreateMap<GET_SChallanDataByID, GET_SChallanDataByIDDto>().ReverseMap();

            CreateMap<GET_Expense, GET_ExpenseDto>().ReverseMap();

            CreateMap<GET_ExpenseCategory, GET_ExpenseCategoryDto>().ReverseMap();

            CreateMap<GET_EmployeeLeavesDate, GET_EmployeeLeavesDateDto>().ReverseMap();

            CreateMap<GET_HRMEmployeesName, GET_HRMEmployeesNameDto>().ReverseMap();

            CreateMap<GET_HRMEmployees , GET_HRMEmployeesDto>().ReverseMap();

            CreateMap<GET_CategoryDropdown , GET_CategoryDropdownDto>().ReverseMap();   

            CreateMap<GET_EMPLOYEE , GET_EMPLOYEEDto >().ReverseMap();

            CreateMap<GET_CompanyDropdown, GET_CompanyDropdownDto>().ReverseMap();

            CreateMap<GET_DesignationDropdown , GET_DesignationDropdownDto>().ReverseMap();

            CreateMap<GET_ACCSESSRIGHTDropdown , GET_ACCSESSRIGHTDropdownDto>().ReverseMap();

            CreateMap<GET_CategoryMasterData , GET_CategoryMasterDataDto>().ReverseMap();

            CreateMap<GET_SubCategoryMaster , GET_SubCategoryMasterDto>().ReverseMap();

            CreateMap<SP_DeleteCategoryMaster , SP_DeleteCategoryMasterDto>().ReverseMap();

            CreateMap<SP_DeleteCategoryMasterRespones , SP_DeleteCategoryMasterResponesDto>().ReverseMap();

            CreateMap<GET_UOMImplication , GET_UOMImplicationDto>().ReverseMap();

            CreateMap<GET_UnitMasterData , GET_UnitMasterDataDto>().ReverseMap();

            CreateMap<GET_Warranty , GET_WarrantyDto>().ReverseMap();

            CreateMap<GET_Deficit , GET_DeficitDto>().ReverseMap();

            CreateMap<GET_DeficitById, GET_DeficitByIdDto>().ReverseMap();

            CreateMap<GET_CounterName, GET_CounterNameDto>().ReverseMap();

            CreateMap<GET_BILLCOUNTS , GET_BILLCOUNTSDto>().ReverseMap();

            CreateMap<GET_StockTransfer, GET_StockTransferDto>().ReverseMap();

            CreateMap<GET_StockTransferD, GET_StockTransferDDto>().ReverseMap();

            CreateMap<GET_StockCount , GET_StockCountDto>().ReverseMap();

            CreateMap<GET_ServiceCharge , GET_ServiceChargeDto>().ReverseMap();

            CreateMap<GET_GateEntry , GET_GateEntryDto>().ReverseMap();

            CreateMap<GET_Transporters , GET_TransportersDto>().ReverseMap();

            CreateMap<GET_VyapariMaster , GET_VyapariMasterDto>().ReverseMap();

            CreateMap<GET_GateFarmer , GET_GateFarmerDto>().ReverseMap();

            CreateMap<GET_PlotMaster , GET_PlotMasterDto>().ReverseMap();

            CreateMap<GET_AUCTIONSHED , GET_AUCTIONSHEDDto>().ReverseMap();

            CreateMap<GET_TOKENNoSearch , GET_TOKENNoSearchDto>().ReverseMap();

            CreateMap<GET_AuctionDetailsById , GET_AuctionDetailsByIdDto>().ReverseMap();

            CreateMap<GET_ST_Implication_ServiceCharge, GET_ST_Implication_ServiceChargeDto>().ReverseMap();

            CreateMap<GET_FamilyDetailsById , GET_FamilyDetailsByIdDto>().ReverseMap();

            CreateMap<GETNewGenrateTokenNo , GETNewGenrateTokenNoDto>().ReverseMap();

            CreateMap<GETItemDropdown , GETItemDropdownDto>().ReverseMap();

            CreateMap<GET_VaypariName , GET_VaypariNameDto>().ReverseMap();

            CreateMap<GET_MarketStore , GET_MarketStoreDto>().ReverseMap();

            CreateMap<GET_RECEIPTMaster , GET_RECEIPTMasterDto>().ReverseMap();

            CreateMap<GET_FARMERSERVICE , GET_FARMERSERVICEDto>().ReverseMap();

            CreateMap<GET_GateEntryDetailById , GET_GateEntryDetailByIdDto>().ReverseMap();

            CreateMap<GET_AuctionDataBill , GET_AuctionDataBillDto>().ReverseMap();

            CreateMap<GET_MainGroup , GET_MainGroupDto>().ReverseMap();

            CreateMap<GET_Subgroup, GET_SubgroupDto>().ReverseMap();

            CreateMap<GET_GeneralGruop , GET_GeneralGruopDto>().ReverseMap();

            CreateMap<GET_ADHAARAPP, GET_ADHAARAPPDto>().ReverseMap();

            CreateMap<GET_ACCOUNT , GET_ACCOUNTDto>().ReverseMap();

            CreateMap<SP_AddUpdFarmerApp , SP_AddUpdFarmerAppDto>().ReverseMap();

            CreateMap<GET_RokdaVyapariMaster , GET_RokdaVyapariMasterDto>().ReverseMap();

            CreateMap<GET_ST_Implication_ShopLOC , GET_ST_Implication_ShopLOCDto>().ReverseMap();

            CreateMap<GET_ShopAlot , GET_ShopAlotDto>().ReverseMap();

            CreateMap<GET_FarmerName , GET_FarmerNameDto>().ReverseMap();

            CreateMap<GET_FarmerCrops , GET_FarmerCropsDto>().ReverseMap();

            CreateMap<GET_ShopName , GET_ShopNameDto>().ReverseMap();

            CreateMap<GET_AuctionDataSearch , GET_AuctionDataSearchDto>().ReverseMap();

            CreateMap<GET_FarmerSearch, GET_FarmerSearchDto>().ReverseMap();

            CreateMap<GET_ReceiptDetailsById, GET_ReceiptDetailsByIdDto>().ReverseMap();

            CreateMap<GET_StandingI_RateA_Farmer, GET_StandingI_RateA_FarmerDto>().ReverseMap();

            CreateMap<GET_StandingI_RateA_Vyapari, GET_StandingI_RateA_VyapariDto>().ReverseMap();

            CreateMap<GET_StandingI_RateA_ShopAlot, GET_StandingI_RateA_ShopAlotDto>().ReverseMap();

            CreateMap<GET_VehicalDetailsById, GET_VehicalDetailsByIdDto>().ReverseMap();

            CreateMap<GET_ServiceChargesById, GET_ServiceChargesByIdDto>().ReverseMap();

            CreateMap<GET_PlotDetailsById, GET_PlotDetailsByIdDto>().ReverseMap();

            CreateMap<GET_District, GET_DistrictDto>().ReverseMap();

            CreateMap<GETNewGenrateBillNo, GETNewGenrateBillNoDto>().ReverseMap();

            CreateMap<GET_VyapariLimit, GET_VyapariLimitDto>().ReverseMap();

            CreateMap<GET_VyapariApprovalList, GET_VyapariApprovalListDto>().ReverseMap();

            CreateMap<GET_NewToken, GET_NewTokenDto>().ReverseMap();

            CreateMap<GET_GateEntryDetailAllApp, GET_GateEntryDetailAllAppDto>().ReverseMap();

            CreateMap<GET_VyapariApprovalByID, GET_VyapariApprovalByIDDto>().ReverseMap();

            CreateMap<GET_AUCTIONSHEDdata , GET_AUCTIONSHEDdataDto>().ReverseMap();

            CreateMap<GET_VaypariNameID, GET_VaypariNameIDDto>().ReverseMap();

            CreateMap<GET_GateEntryDetailList, GET_GateEntryDetailListDto>().ReverseMap();

            CreateMap<GET_VyapariSanction, GET_VyapariSanctionDto>().ReverseMap();

            CreateMap<GET_GateEntryDetail , GET_GateEntryDetailDto>().ReverseMap();

            CreateMap<GET_FarmerFromFamilyDetails , GET_FarmerFromFamilyDetailsDto > ().ReverseMap();

            CreateMap<GET_GateBankDetails, GET_GateBankDetailsDto>().ReverseMap();

            CreateMap<GET_VyapariDropdown, GET_VyapariDropdownDto>().ReverseMap();

            CreateMap<GET_GenrateTokenNO, GET_GenrateTokenNODto>().ReverseMap();

            CreateMap<GET_BankName, GET_BankNameDto>().ReverseMap();

            CreateMap<GET_RECEIPTDataBill, GET_RECEIPTDataBillDto>().ReverseMap();  

            CreateMap<GET_VYAPARISERVICE, GET_VYAPARISERVICEDto>().ReverseMap();

            CreateMap<GET_GateCashInvert , GET_GateCashInvertDto>().ReverseMap();

            CreateMap<SP_DeleteCashInvert , SP_DeleteCashInvertDto>().ReverseMap();

            CreateMap<GET_GenrateACCNO, GET_GenrateACCNODto>().ReverseMap();

            CreateMap<GET_Notification, GET_NotificationDto>().ReverseMap();

            CreateMap<GET_CheckMobileApp , GET_CheckMobileAppDto>().ReverseMap();

            CreateMap<GET_DashCount , GET_DashCountDto>().ReverseMap();

            CreateMap<GET_StandingInstrcution , GET_StandingInstrcutionDto>().ReverseMap();

            CreateMap<GET_GateEntryDetailVehno , GET_GateEntryDetailVehnoDto>().ReverseMap();

            CreateMap<GET_AccountACCTM ,  GET_AccountACCTMDto>().ReverseMap();

            CreateMap<GET_AdminVideo , GET_AdminVideoDto>().ReverseMap();

            CreateMap<GET_Advertisement , GET_AdvertisementDto>().ReverseMap();

            CreateMap<GET_RECEIPTDataBillByToken , GET_RECEIPTDataBillByTokenDto>().ReverseMap();

            CreateMap<GET_VapariStandingInstrcution, GET_VapariStandingInstrcutionDto>().ReverseMap();

            CreateMap<GET_GateCashSummary , GET_GateCashSummaryDto>().ReverseMap();

            CreateMap<GET_VoucherMaster , GET_VoucherMasterDto>().ReverseMap();

            CreateMap<GET_FarmerAddedByTranspoter , GET_FarmerAddedByTranspoterDto>().ReverseMap();

            CreateMap<GET_FarmerAddedByTranspoterAll , GET_FarmerAddedByTranspoterAllDto>().ReverseMap();

            CreateMap<GET_AUCTIONSHEDVAPARI, GET_AUCTIONSHEDVAPARIDto>().ReverseMap();

            CreateMap<GET_RECEIPTMasterAUCTION , GET_RECEIPTMasterAUCTIONDto>().ReverseMap();

            CreateMap<GET_VoucherMasterJama , GET_VoucherMasterJamaDto>().ReverseMap();

            CreateMap<GET_VoucherMasterNave , GET_VoucherMasterNaveDto>().ReverseMap();

            CreateMap<GET_CashCounter , GET_CashCounterDto>().ReverseMap();

            CreateMap<GET_CreateBill , GET_CreateBillDto>().ReverseMap();

            CreateMap<GET_BillNoSearch , GET_BillNoSearchDto>().ReverseMap();

            CreateMap<GET_FarmerSearchBYAadhaar , GET_FarmerSearchBYAadhaarDto>().ReverseMap();

            CreateMap<GET_FarmerBillCharges , GET_FarmerBillChargesdDto>().ReverseMap();

            CreateMap<GET_CropItem , GET_CropItemDto>().ReverseMap();

            CreateMap<GET_Voucher , GET_VoucherDto>().ReverseMap();

            CreateMap<SP_AddUpdUserMasters , SP_AddUpdUserMastersDto>().ReverseMap();

            CreateMap<GET_VyapariApp , GET_VyapariAppDto>().ReverseMap();

            CreateMap<GET_FARMER, GET_FARMERDto>().ReverseMap();

            CreateMap<GET_VYAPARIDUEDATA, GET_VYAPARIDUEDATADto>().ReverseMap();

            CreateMap<GET_FARMERBILL , GET_FARMERBILLDto>().ReverseMap();

            CreateMap<GET_CashDetails , GET_CashDetailsDto>().ReverseMap();

            CreateMap<GET_CreateBill_Vyapari , GET_CreateBill_VyapariDto>().ReverseMap();

            CreateMap<GET_VyapariBillCharges , GET_VyapariBillChargesDto>().ReverseMap();

            CreateMap<GET_VyapariAppFarmer , GET_VyapariAppFarmerDto>().ReverseMap();

            CreateMap<SP_AddUpdGateEntryDetailApp, SP_AddUpdGateEntryDetailAppDto>().ReverseMap();

            CreateMap<GET_VyapariBillMasterData , GET_VyapariBillMasterDataDto>().ReverseMap();

            CreateMap<GET_VyapariBillMaster , GET_VyapariBillMasterDto>().ReverseMap(); 

            CreateMap<GET_CheckUserSession , GET_CheckUserSessionDto>().ReverseMap();

            CreateMap<GET_FBILLSERVICES, GET_FBILLSERVICESDto>().ReverseMap();

            CreateMap<GET_VyapariBillMasterAuction, GET_VyapariBillMasterAuctionDto>().ReverseMap();

            CreateMap<GetAdminAdvertisement , GetAdminAdvertisementDto>().ReverseMap();

            CreateMap<GetAdminVideoApp , GetAdminVideoAppDto>().ReverseMap();

            CreateMap<GetItemImage , GetItemImageDto>().ReverseMap();

            CreateMap<GET_RECEIPTMasterCashCounter , GET_RECEIPTMasterCashCounterDto>().ReverseMap();

            CreateMap<GET_GateEntryDataByVehno , GET_GateEntryDataByVehnoDto>().ReverseMap();

            CreateMap<GET_Report_Vyapari , GET_Report_VyapariDto>().ReverseMap();

            CreateMap<GET_Report_farmer, GET_Report_farmerDto>().ReverseMap();

            CreateMap<GET_VyapariLogin, GET_VyapariLoginDto>().ReverseMap();

            CreateMap<GET_Report_Auction , GET_Report_AuctionDto>().ReverseMap();

            CreateMap<GET_CashCounterMatchData , GET_CashCounterMatchDataDto>().ReverseMap();

            CreateMap<GET_ActiveVyapariDetails , GET_ActiveVyapariDetailsDto>().ReverseMap();

            CreateMap<GET_RECEIPTMasterDataMatch, GET_RECEIPTMasterDataMatchDto>().ReverseMap();

            CreateMap<GET_FarmerDetails , GET_FarmerDetailsDto>().ReverseMap();

            CreateMap<GET_AuctionDataByToken , GET_AuctionDataByTokenDto>().ReverseMap();

            CreateMap<GET_VoucherJama , GET_VoucherJamaDto>().ReverseMap();

            CreateMap<GET_VoucherNave , GET_VoucherNaveDto>().ReverseMap();

            CreateMap<GET_ColdStorageInward , GET_ColdStorageInwardDto>().ReverseMap();

            CreateMap<GET_PreCooling , GET_PreCoolingDto>().ReverseMap();

            CreateMap<GET_Caret , GET_CaretDto>().ReverseMap();

            CreateMap<GET_VoucherJamaNave , GET_VoucherJamaNaveDto>().ReverseMap();

            CreateMap<GET_ST_Implication_Day , GET_ST_Implication_DayDto>().ReverseMap();

            CreateMap<GET_Complaint , GET_ComplaintDto>().ReverseMap();

            CreateMap<GET_CountPrecooling , GET_CountPrecoolingDto>().ReverseMap();

            CreateMap<GET_RewardPoint , GET_RewardPointDto>().ReverseMap();

            CreateMap<GET_AdvertisementApp, GET_AdvertisementAppDto>().ReverseMap();

            CreateMap<Get_AdminVideoApp, Get_AdminVideoAppDto>().ReverseMap();

            CreateMap<GET_VyapariApprove, GET_VyapariApproveDto>().ReverseMap();

            CreateMap<GET_CompanyInfo, GET_CompanyInfoDto>().ReverseMap();

            CreateMap<GET_OTP, GET_OTPDto>().ReverseMap();

            CreateMap<GET_FARMERAPPBill, GET_FARMERAPPBillDto>().ReverseMap();

            CreateMap<GET_CreateBillApp, GET_CreateBillAppDto>().ReverseMap();

            CreateMap<SP_CheckAadhaardetail, SP_CheckAadhaardetailDto>().ReverseMap();

            CreateMap<GET_Report_Farmerbill, GET_Report_FarmerbillDto>().ReverseMap();

            CreateMap<GET_Report_VyapariLimit, GET_Report_VyapariLimitDto>().ReverseMap();

            CreateMap<GET_Report_VyapariBill, GET_Report_VyapariBillDto>().ReverseMap();

            CreateMap<GET_HIGHESTFRUITRATEEAuction, GET_HIGHESTFRUITRATEEAuctionDto>().ReverseMap();

            CreateMap<SP_AddUpdVyapariSlabDetail, SP_AddUpdVyapariSlabDetailDto>().ReverseMap();

            CreateMap<GET_VyapariSlabMaster, GET_VyapariSlabMasterDto>().ReverseMap();

            CreateMap<GET_GateCashSummaryCashCount, GET_GateCashSummaryCashCountDto>().ReverseMap();

            CreateMap<GET_VyapariSlabMasterData, GET_VyapariSlabMasterDataDto>().ReverseMap();

            CreateMap<GET_DashboardCount, GET_DashboardCountDto>().ReverseMap();

            CreateMap<GET_TopFarmerOnCrates, GET_TopFarmerOnCratesDto>().ReverseMap();

            CreateMap<GET_CreateFarmerBillVyapari, GET_CreateFarmerBillVyapariDto>().ReverseMap();

            CreateMap<GET_FarmerBillChargesVyapari, GET_FarmerBillChargesVyapari>().ReverseMap();

            CreateMap<GET_DayEndInfo, GET_DayEndInfoDto>().ReverseMap();

            CreateMap<GET_TopVyapariOnCrates, GET_TopVyapariOnCratesDto>().ReverseMap();

            CreateMap<GET_VyapariSchemeName, GET_VyapariSchemeNameDto>().ReverseMap();

            CreateMap<GET_CashDetailsNearbyCash, GET_CashDetailsNearbyCashDto>().ReverseMap();

            CreateMap<GET_FarmerFamilyDetail, GET_FarmerFamilyDetailDto>().ReverseMap();

            CreateMap<GET_RokadVyapariSchemeName, GET_RokadVyapariSchemeNameDto>().ReverseMap();

            CreateMap<GET_AuctionUser, GET_AuctionUserDto>().ReverseMap();

            CreateMap<GET_RokdaVyapari, GET_RokdaVyapariDto>().ReverseMap();

            CreateMap<GET_VyaparijamaByVyapariDroDown, GET_VyaparijamaByVyapariDroDownDto>().ReverseMap();
            
            CreateMap<GET_VyapariJamaMaster, GET_VyapariJamaMasterDto>().ReverseMap();

            CreateMap<GET_TranspotersGateEntryDetailList, GET_TranspotersGateEntryDetailListDto>().ReverseMap();

            CreateMap<GET_GateEntry_VT, GET_GateEntry_VTDto>().ReverseMap();

            CreateMap<GET_PrecoolingSlab, GET_PrecoolingSlabDto>().ReverseMap();

            CreateMap<GET_VyapariDueBharanaData, GET_VyapariDueBharanaDataDto>().ReverseMap();

            CreateMap<GET_DashBoardRateAuction, GET_DashBoardRateAuctionDto>().ReverseMap();

            CreateMap<GET_SCHEMENAME, GET_SCHEMENAMEDto>().ReverseMap();

            CreateMap<GET_CropWeekData, GET_CropWeekDataDto>().ReverseMap();

            CreateMap<GET_CropMonthData, GET_CropMonthDataDto>().ReverseMap();

            CreateMap<GET_VyapariSchemeDetails, GET_VyapariSchemeDetailsDto>().ReverseMap();

            CreateMap<GET_ColdStorageOutWard, GET_ColdStorageOutWardDto>().ReverseMap();

            CreateMap<GET_Cashier_Balance, GET_Cashier_BalanceDto>().ReverseMap();

            CreateMap<GET_TopfarmerOnPrice, GET_TopfarmerOnPriceDto>().ReverseMap();

            CreateMap<GET_ColdStorageOutWardById, GET_ColdStorageOutWardByIdDto>().ReverseMap();

            CreateMap<GET_OnlineAmount, GET_OnlineAmountDto>().ReverseMap();

            CreateMap<GET_VyapariVapasiDetails, GET_VyapariVapasiDetailsDto>().ReverseMap();

            CreateMap<GET_VyapariDue, GET_VyapariDueDto>().ReverseMap();

            CreateMap<SP_DeleteVyapariVapasiDetails, SP_DeleteVyapariVapasiDetailsDto>().ReverseMap();

            CreateMap<GET_RegulerVaypariName, GET_RegulerVaypariNameDto>().ReverseMap();

            CreateMap<GET_GetRTGS, GET_GetRTGSDto>().ReverseMap();

            CreateMap<GET_FarmerForNotifications, GET_FarmerForNotificationsDto>().ReverseMap();

            CreateMap<GET_TransportersForNotifications, GET_TransportersForNotificationsDto>().ReverseMap();

            CreateMap<GET_AdminNotificationForFarmer, GET_AdminNotificationForFarmerDto>().ReverseMap();

            CreateMap<GET_AdminNotificationForTransporter, GET_AdminNotificationForTransporterDto>().ReverseMap();

            CreateMap<GET_VyapariBharanaData, GET_VyapariBharanaDataDto>().ReverseMap();

            CreateMap<SP_DeleteVyapariDue, SP_DeleteVyapariDueDto>().ReverseMap();

            CreateMap<GET_SaveNotification, GET_SaveNotificationDto>().ReverseMap();

            CreateMap<MarkSheet, MarkSheetDto>();

            CreateMap<GET_VyapariAuctionView, GET_VyapariAuctionViewDto>();

            CreateMap<GET_FBillPrint, GET_FBillPrintDto>();

            CreateMap<GET_RecentFarmerToken, GET_RecentFarmerTokenDto>();

            CreateMap<GET_PARKING, GET_PARKINGDto>().ReverseMap();

            CreateMap<GET_GateEntryDetailByIdApp, GET_GateEntryDetailByIdAppDto>().ReverseMap();

            CreateMap<GET_GateDate, GET_GateDateDto>().ReverseMap();

            CreateMap<GET_Report_VyapariVakkal, GET_Report_VyapariVakkalDto>().ReverseMap();

            CreateMap<GET_RecentFarmerTokenApp, GET_RecentFarmerTokenAppDto>().ReverseMap();

            CreateMap<GET_VyapariBillChargesBill, GET_VyapariBillChargesBillDto>().ReverseMap();

            CreateMap<GET_CheckGateEntryVerify, GET_CheckGateEntryVerifyDto>().ReverseMap();

            CreateMap<GET_PAIDReport, GET_PAIDReportDto>().ReverseMap();

            CreateMap<GET_CheckVehicalExist, GET_CheckVehicalExistDto>().ReverseMap();

            CreateMap<SP_AddUpdTransportersApp, SP_AddUpdTransportersAppDto>().ReverseMap();

            CreateMap<SP_AddUpdTransportersApp2, SP_AddUpdTransportersApp2Dto>().ReverseMap();

            CreateMap<GetVyapriDetailsReport, GetVyapriDetailsReportDto>().ReverseMap();

            CreateMap<GET_VyapariPurchaseBillR, GET_VyapariPurchaseBillRDto>().ReverseMap();

            CreateMap<GET_Report_Services, GET_Report_ServicesDto>().ReverseMap();

            CreateMap<GET_Report_DayAuction, GET_Report_DayAuctionDto>().ReverseMap();

            CreateMap<GET_REPORT_S, GET_REPORT_SDto>().ReverseMap();

            CreateMap<GET_CheckFarmarFaid, GET_CheckFarmarFaidDto>().ReverseMap();

            CreateMap<GET_CheckTranspoterTaid, GET_CheckTranspoterTaidDto>().ReverseMap();

            CreateMap<GET_NotifyTransporterOnTokenGeneration, GET_NotifyTransporterOnTokenGenerationDto>().ReverseMap();

            CreateMap<GET_NotifyFarmerOnTokenGeneration, GET_NotifyFarmerOnTokenGenerationDto>().ReverseMap();

            CreateMap<GET_RegulerVyapriBharana, GET_RegulerVyapriBharanaDto>().ReverseMap();

            CreateMap<GET_FarmerTokenNotification, GET_FarmerTokenNotificationDto>().ReverseMap();

            CreateMap<GET_VyapariTransectionReport, GET_VyapariTransectionReportDto>().ReverseMap();

            CreateMap<GET_CategoryMasterCase, GET_CategoryMasterCaseDto>().ReverseMap();

            CreateMap<GET_TyreProductMaster, GET_TyreProductMasterDto>().ReverseMap();

            CreateMap<GET_CustomerName, GET_CustomerNameDto>().ReverseMap();

            CreateMap<GET_DelarRate, GET_DelarRateDto>().ReverseMap();

            CreateMap<GET_ProductDName, GET_ProductDNameDto>().ReverseMap();

            CreateMap<GET_DealerSearch, GET_DealerSearchDto>().ReverseMap();

            CreateMap<GET_SchedulerMaster, GET_SchedulerMasterDto>().ReverseMap();

            CreateMap<GET_TyreCustomerPartners, GET_TyreCustomerPartnersDto>().ReverseMap();

            CreateMap<GET_VisitorMaster, GET_VisitorMasterDto>().ReverseMap();

            CreateMap<SP_DeleteVisitorMaster, SP_DeleteVisitorMasterDto>().ReverseMap();

            CreateMap<GET_VisitorData, GET_VisitorDataDto>().ReverseMap();

            CreateMap<GET_CreateFBillVyapari, GET_CreateFBillVyapariDto>().ReverseMap();

            CreateMap<GET_VBillChargesBill, GET_VBillChargesBillDto>().ReverseMap();

            CreateMap<GET_ShedulerMasterData, GET_ShedulerMasterDataDto>().ReverseMap();

            CreateMap<GET_PackingMaterial, GET_PackingMaterialDto>().ReverseMap();

            CreateMap<GET_CategoryDropdownInv, GET_CategoryDropdownInvDto>().ReverseMap();

            CreateMap<GET_PackingMaterialDropdown, GET_PackingMaterialDropdownDto>().ReverseMap();

            CreateMap<GET_CropInvertData, GET_CropInvertDataDto>().ReverseMap();

            CreateMap<GET_FARMERFROMCONTACT, GET_FARMERFROMCONTACTDto>().ReverseMap();

            CreateMap<GET_ShedulerDetailData, GET_ShedulerDetailDataDto>().ReverseMap();

            CreateMap<GET_CropInvertData1, GET_CropInvertData1Dto>().ReverseMap();

            CreateMap<GET_CropInvertDetailsById, GET_CropInvertDetailsByIdDto>().ReverseMap();

            CreateMap<GET_CropInvertMaster, GET_CropInvertMasterDto>().ReverseMap();

            CreateMap<GET_DayBookData, GET_DayBookDataDto>().ReverseMap();

            CreateMap<ReceiptRequest, ReceiptDto>().ReverseMap();

            CreateMap<GET_CropInvertBill, GET_CropInvertBillDto>().ReverseMap();

            CreateMap<GET_CropInvertDateCheck, GET_CropInvertDateCheckDto>().ReverseMap();

            CreateMap<SP_TrialBalanceData, SP_TrialBalanceDataDto>().ReverseMap();

            CreateMap<GET_TrialBalanceData, GET_TrialBalanceDataDto>().ReverseMap();

            CreateMap<GET_QuatationSRNo, GET_QuatationSRNoDto>().ReverseMap();

            CreateMap<GET_ProfitLossData, GET_ProfitLossDataDto>().ReverseMap();

            CreateMap<SP_DeletePGoodReceiptNote, SP_DeletePGoodReceiptNoteDto>().ReverseMap();

            CreateMap<SP_AddUpdCropBill, SP_AddUpdCropBillDto>().ReverseMap();

            CreateMap<SP_AddUpdCropBillDetail, SP_AddUpdCropBillDetailDto>().ReverseMap();

            CreateMap<GET_CropBillDetail, GET_CropBillDetailDto>().ReverseMap();

            CreateMap<GET_CropBill, GET_CropBillDto>().ReverseMap();

            CreateMap<GET_BalanceshitData, GET_BalanceshitDataDto>().ReverseMap();

            CreateMap<GET_GenralAccount, GET_GenralAccountDto>().ReverseMap();

            CreateMap<GET_LedgerData, GET_LedgerDataDto>().ReverseMap();

            CreateMap<GET_TraderNameDropdown, GET_TraderNameDropdownDto>().ReverseMap();

            CreateMap<GET_PQuatationNoCheck, GET_PQuatationNoCheckDto>().ReverseMap();

            CreateMap<GET_POrderNoCheck, GET_POrderNoCheckDto>().ReverseMap();

            CreateMap<GET_PRequisitionNoCheck, GET_PRequisitionNoCheckDto>().ReverseMap();

            CreateMap<SP_AddUpdtStockLoadingVehicleMaster, SP_AddUpdtStockLoadingVehicleMasterDto>().ReverseMap();

            CreateMap<GET_CropVehicleLoadingData, GET_CropVehicleLoadingDataDto>().ReverseMap();

            CreateMap<GET_PChallanNoCheck, GET_PChallanNoCheckDto>().ReverseMap();

            CreateMap<SP_AddUpdStockLoadingVehicleDetail, SP_AddUpdStockLoadingVehicleDetailDto>().ReverseMap();

            CreateMap<GET_StockLoadingVehicleMaster, GET_StockLoadingVehicleMasterDto>().ReverseMap();

            CreateMap<SP_DeleteStockLoadingVehicleMaster, SP_DeleteStockLoadingVehicleMasterDto>().ReverseMap();

            CreateMap<GET_SQuatationForProceed, GET_SQuatationForProceedDto>().ReverseMap();

            CreateMap<GET_MasterDetailInvertData, GET_MasterDetailInvertDataDto>().ReverseMap();

            CreateMap<GET_chalanNO, GET_chalanNODto>().ReverseMap();

            CreateMap<ApiResponseMessage, ApiResponseMessageDto>().ReverseMap();

            CreateMap<GET_BillNO, GET_BillNODto>().ReverseMap();

            CreateMap<GET_StockLoadingVehicleDetail, GET_StockLoadingVehicleDetailDto>().ReverseMap();

            CreateMap<GET_CropTransportInvoiceData, GET_CropTransportInvoiceDataDto>().ReverseMap();

            CreateMap<GET_VisitorMasterApproval, GET_VisitorMasterApprovalDto>().ReverseMap();

            CreateMap<GET_ScheduleDetails, GET_ScheduleDetailsDto>().ReverseMap();

            CreateMap<SP_AddUpdSabhasadMaster, SP_AddUpdSabhasadMasterDto>().ReverseMap();

            CreateMap<GET_SabhasadMaster, GET_SabhasadMasterDto>().ReverseMap();

            CreateMap<GET_SFamilyInformation, GET_SFamilyInformationDto>().ReverseMap();

            CreateMap<GET_SAgriculturalInformation, GET_SAgriculturalInformationDto>().ReverseMap();

            CreateMap<GET_SCropInformation, GET_SCropInformationDto>().ReverseMap();

            CreateMap<GET_SFertilizersName, GET_SFertilizersNameDto>().ReverseMap();

            CreateMap<GET_SChemicalName, GET_SChemicalNameDto>().ReverseMap();

            CreateMap<GET_SContactNumberCheck, GET_SContactNumberCheckDto>().ReverseMap();

            CreateMap<GET_SabhasadContactNumberCheck, GET_SabhasadContactNumberCheckDto>().ReverseMap();

            CreateMap<GET_AllTransectionsData, GET_AllTransectionsDataDto>().ReverseMap();

            CreateMap<GET_CropBillView, GET_CropBillViewDto>().ReverseMap();

            CreateMap<GET_CropTransportInvoice, GET_CropTransportInvoiceDto>().ReverseMap();

            CreateMap<GET_TotalPurchaseReport, GET_TotalPurchaseReportDto>().ReverseMap();

            CreateMap<GET_Report_Sales, GET_Report_SalesDto>().ReverseMap();

            CreateMap<GET_CropBillData, GET_CropBillDataDto>().ReverseMap();

            CreateMap<GET_FarmerBillData, GET_FarmerBillDataDto>().ReverseMap();

            CreateMap<GET_SingleRowCropInvertDetail, GET_SingleRowCropInvertDetailDto>().ReverseMap();

            CreateMap<GET_FarmerTranData, GET_FarmerTranDataDto>().ReverseMap();

            CreateMap<GET_FarmerCropBillData, GET_FarmerCropBillDataDto>().ReverseMap();

            CreateMap<GET_FarmerLedgerReport, GET_FarmerLedgerReportDto>().ReverseMap();

            CreateMap<GET_FarmerPaymentSummary, GET_FarmerPaymentSummaryDto>().ReverseMap();

            CreateMap<GET_FarmerAdvance, GET_FarmerAdvanceDto>().ReverseMap();

            CreateMap<GET_TransportBillData, GET_TransportBillDataDto>().ReverseMap();

            CreateMap<SP_DeleteSchedulerDetail, SP_DeleteSchedulerDetailDto>().ReverseMap();

            CreateMap<GET_CustomerTransportBillData, GET_CustomerTransportBillDataDto>().ReverseMap();

            CreateMap<GET_CustomerTranData, GET_CustomerTranDataDto>().ReverseMap();

            CreateMap<GET_AllTranDataCustomer, GET_AllTranDataCustomerDto>().ReverseMap();

            CreateMap<GET_ACCSESSRIGHTDropdownSFC, GET_ACCSESSRIGHTDropdownSFCDto>().ReverseMap();

            CreateMap<GET_VechicleProfitReport, GET_VechicleProfitReportDto>().ReverseMap();

            CreateMap<GET_CropDirectInvertMaster, GET_CropDirectInvertMasterDto>().ReverseMap();

            CreateMap<GET_CropDirectInvertDetail, GET_CropDirectInvertDetailDto>().ReverseMap();

            CreateMap<GET_SChallanProceed, GET_SChallanProceedDto>().ReverseMap();

            CreateMap<GET_SBillNo, GET_SBillNoDto>().ReverseMap();

            CreateMap<GET_SReturnBillNo, GET_SReturnBillNoDto>().ReverseMap();

            CreateMap<GET_SuggestPhoneNumber, GET_SuggestPhoneNumberDto>().ReverseMap();

            CreateMap<GET_PReturnBillNo, GET_PReturnBillNoDto>().ReverseMap();

            CreateMap<GET_CheckSRNO_Exist, GET_CheckSRNO_ExistDto>().ReverseMap();

            CreateMap<GET_challan_from_Return_master, GET_challan_from_Return_masterDto>().ReverseMap();

            CreateMap<GET_CustomerLedger, GET_CustomerLedgerDto>().ReverseMap();

            CreateMap<GET_SalesReport, GET_SalesReportDto>().ReverseMap();


        }

    }
    
}
