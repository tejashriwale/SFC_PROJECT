using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using matrimonybackendApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using PdfSharpCore.Fonts;
using Project.Controllers;
using Project.Data;
using Project.Mapping;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;
using Project.Service;
using System.Text;


//using Project.Service;

var builder = WebApplication.CreateBuilder(args);


// Add HttpClient
builder.Services.AddHttpClient<StateController>();

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    options.AddPolicy("Production", policy =>
    {
        policy.WithOrigins("https://www.shramjeevanagrofarm.com")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});



builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("shadimuharathConStrt")));


builder.Services.AddScoped<IWareHouseRepository, SqlWareHouseRepository>();

builder.Services.AddScoped<IHSNMasterRepository, SqlHSNMasterRepository>();

builder.Services.AddScoped<ICustomersPartnersRepository, SqlCustomersPartnersRepository>();

builder.Services.AddScoped<IStatecodeRepository, SqlStatecodeRepository>();

builder.Services.AddScoped<IUnitConversionRepository, SqlUnitConversionRepository>();

builder.Services.AddScoped<IRackMasterRepository, SqlRackMasterRepository>();

builder.Services.AddScoped<IDepartmentMasterRepository, SqlIDepartmentMasterRepository>();

builder.Services.AddScoped<IAccessRightsRepository, SqlAccessRightsRepository>();

builder.Services.AddScoped<IBarcodeSettingsRepository, SqlBarcodeSettingsRepository>();

builder.Services.AddScoped<IBOMPackingRepository, SqlBOMPackingRepository>();

builder.Services.AddScoped<ICounterMasterRepository, SqlICounterMasterRepository>();

builder.Services.AddScoped<IPackingMasterRepository, SqlPackingMasterRepository>();

builder.Services.AddScoped<IProductMasterRepository, SqlProductMasterRepository>();

builder.Services.AddScoped<IReturnPolicyRepository, SqlReturnPolicyRepository>();

builder.Services.AddScoped<IUserMastersRepository, SqlUserMastersRepository>();

builder.Services.AddScoped<I_GET_AccessRights_Repository, Sql_GET_AccessRights_Repository>();

builder.Services.AddScoped<I_GET_BarcodeSettings_Repository, Sql_GET_BarcodeSettings_Repository>();

builder.Services.AddScoped<IGET_BOMPackingRepository, SqlGET_BOMPackingRepository>();

builder.Services.AddScoped<I_GET_CounterMaster_Repository, Sql_GET_CounterMaster_Repository>();

builder.Services.AddScoped<IGET_CustomersPartnersRepository, Sql_GETCustomersPartnersRepository>();

builder.Services.AddScoped<I_GET_DepartmentMaster_Repository, Sql_GET_DepartmentMaster_Repository>();

builder.Services.AddScoped<I_GET_HSNMasters_Repository, Sql_GET_HSNMasters_Repository>();

builder.Services.AddScoped<I_GET_PackingMasters_Repository, Sql_GET_PackingMasters_Repository>();

builder.Services.AddScoped<I_GET_ProductMaster_Repository, Sql_GET_ProductMaster_Repository>();

builder.Services.AddScoped<I_GET_RackMaster_Repository, Sql_GET_RackMaster_Repository>();

builder.Services.AddScoped<I_GET_ReturnPolicy_Repository, Sql_GET_ReturnPolicy_Repository>();

builder.Services.AddScoped<I_GET_ServicesMaster_Repository, Sql_GET_ServicesMaster_Repository>();

builder.Services.AddScoped<I_GET_StateCodeMasters_Repository, Sql_GET_StateCodeMasters_Repository>();

builder.Services.AddScoped<I_GET_UnitConversionMasters_Repository, Sql_GET_UnitConversionMasters_Repository>();

builder.Services.AddScoped<IImplicationsRepository, SqlImplicationsRepository>();

builder.Services.AddScoped<I_GET_UserMasters_Repository, Sql_GET_UserMasters_Repository>();

builder.Services.AddScoped<I_GET_UserProfile_Repository, Sql_GET_UserProfile_Repository>();

builder.Services.AddScoped<I_GET_WarehouseMaster_Repository, Sql_GET_WarehouseMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteServiceMaster_Repository, Sql_SP_DeleteServiceMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteHSNMaster_Repository, Sql_SP_DeleteHSNMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteAccessRights_Repository, Sql_SP_DeleteAccessRights_Repository>();

builder.Services.AddScoped<I_SP_DeleteBarcodeSettings_Repository, Sql_SP_DeleteBarcodeSettings_Repository>();

builder.Services.AddScoped<ISP_DeleteBOMPackingRepository, SqlSP_DeleteBOMPackingRepository>();

builder.Services.AddScoped<I_SP_DeleteCounterMaster_Repository, Sql_SP_DeleteCounterMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteCustomersPartners_Repository, Sql_SP_DeleteCustomersPartners_Repository>();

builder.Services.AddScoped<I_SP_DeleteDepartmentMaster_Repository, Sql_SP_DeleteDepartmentMaster_Repository>();

builder.Services.AddScoped<I_SP_DeletePackingMasters_Repository, Sql_SP_DeletePackingMasters_Repository>();

builder.Services.AddScoped<I_SP_DeleteProductMaster_Repository, Sql_SP_DeleteProductMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteRackMaster_Repository, Sql_SP_DeleteRackMaster_Repository>();

builder.Services.AddScoped<I_SP_DeleteReturnPolicy_Repository, Sql_SP_DeleteReturnPolicy_Repository>();

builder.Services.AddScoped<I_SP_DeleteStateCodeMasters_Repository, Sql_SP_DeleteStateCodeMasters_Repository>();

builder.Services.AddScoped<IDeleteUnitConversionMastersRepository, SqlDeleteUnitConversionMastersRepository>();

builder.Services.AddScoped<IDeleteWarehouseMasterRepository, SqlDeleteWarehouseMasterRepository>();

builder.Services.AddScoped<IDeleteUserMastersRepository, SqlDeleteUserMastersRepository>();

builder.Services.AddScoped<IGSTUPDATERepository, SqlGSTUPDATERepository>();

builder.Services.AddScoped<IWAREHOUSENAMERepository, SqlWAREHOUSENAMERepository>();

builder.Services.AddScoped<IHSNGSTDetailsRepository, SqlHSNGSTDetailsRepository>();

builder.Services.AddScoped<I_SP_AddImplicationTitle_Repository, Sql_SP_AddImplicationTitle_Repository>();

builder.Services.AddScoped<IGET_HSNCodeRepository, SqlGET_HSNCodeRepository>();

builder.Services.AddScoped<IGET_UserProfileSearchRepository, SqlGET_UserLoginesarchRepository>();

builder.Services.AddScoped<IStatePincodeRepository, SqlStatePincodeRepository>();

builder.Services.AddScoped<IAddUpdCompanyRepository, SqlAddUpdCompanyRepository>();

builder.Services.AddScoped<ISP_AddUpdCategoryMasterRepository, SqlSP_AddUpdCategoryMasterRepository>();

builder.Services.AddScoped<ISP_AddPBillDetailsReposioty, SqlSP_AddPBillDetailsReposioty>();

builder.Services.AddScoped<IAddUpdPBillMasterRepository, SqlAddUpdPBillMasterRepository>();

builder.Services.AddScoped<IAddUpdPChallanDetailsRepository, SqlAddUpdPChallanDetailsRepository>();

builder.Services.AddScoped<IAddUpdPChallanMasterRepository, SqlAddUpdPChallanMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdPOrderDetailsRepository, SqlSP_AddUpdPOrderDetailsRepository>();

builder.Services.AddScoped<IAddUpdPOrderMasterrepository, SqlAddUpdPOrderMasterrepository>();

builder.Services.AddScoped<IAddUpdPRequisitionDetailsRepository, SqlAddUpdPRequisitionDetailsRepository>();

builder.Services.AddScoped<IAddUpdPRequisitionMasterRepository, SqlAddUpdPRequisitionMasterRepository>();

builder.Services.AddScoped<IAddUpdPReturnDetailsRepository, SqlAddUpdPReturnDetailsRepository>();

builder.Services.AddScoped<IAddUpdPReturnMasterRepository, SqlAddUpdPReturnMasterRepository>();

builder.Services.AddScoped<IAddUpdQuatationDetailsRepository, SqlAddUpdQuatationDetailsRepository>();

builder.Services.AddScoped<IAddQuatationMasterRepository, SqlAddQuatationMasterRepository>();

builder.Services.AddScoped<IAddGoodReceiptNoteAssuranceRepository, SqlAddGoodReceiptNoteAssuranceRepository>();

builder.Services.AddScoped<IAddGoodReceiptNoteDetailsRepository, SqlAddGoodReceiptNoteDetailsRepository>();

builder.Services.AddScoped<IAddGoodReceiptNoteMasterRepository, SqlAddGoodReceiptNoteMasterRepository>();

builder.Services.AddScoped<IGET_CategoryRepository, SqlGET_CategoryRepository>();

builder.Services.AddScoped<IGET_CategoryCustomerRepository, SqlIGET_CategoryCustomerRepository>();

builder.Services.AddScoped<IGET_CategoryProductRepository, SqlGET_CategoryProductRepository>();

builder.Services.AddScoped<IGET_StateCodeDistinctRepository, SqlGET_StateCodeDistinctRepository>();

builder.Services.AddScoped<IGET_PRequisitionMasterRepositroy, SqlGET_PRequisitionMasterRepositroy>();

builder.Services.AddScoped<IGET_PRequisitionDetailsRepository, SqlGET_PRequisitionDetailsRepository>();

builder.Services.AddScoped<IGET_CustomerDetailsRepository, SqlIGET_CustomerDetailsRepository>();

builder.Services.AddScoped<IGET_QuatationMasterRepository, SqlGET_QuatationMasterRepository>();

builder.Services.AddScoped<IAddAnalysisRepository, SqlAddAnalysisRepository>();

builder.Services.AddScoped<IGET_AnalysisRepository, SqlGET_AnalysisRepository>();

builder.Services.AddScoped<IGET_PRequisitionNosearchRepository, SqlGET_PRequisitionNosearchRepository>();

builder.Services.AddScoped<IGET_PRequisitionNumberSearchRepository, SqlGET_PRequisitionNumberSearchRepository>();

builder.Services.AddScoped<IGET_AnalyzeDataRepository, SqlIGET_AnalyzeDataRepository>();

builder.Services.AddScoped<IGET_QuatationMasterSearchRepository, SqlGET_QuatationMasterSearchRepository>();

builder.Services.AddScoped<IGET_POrderMasterRepository, SqlGET_POrderMasterRepository>();

builder.Services.AddScoped<IGET_PurchaseOrderDataByIdRepository, SqlGET_PurchaseOrderDataByIdRepository>();

builder.Services.AddScoped<IGET_QuatationSearchNoRepository, SqlGET_QuatationSearchNoRepository>();

builder.Services.AddScoped<IGET_CustomerDetailsByIDRepository, SqlGET_CustomerDetailsByIDRepository>();

builder.Services.AddScoped<ISP_AddUpdSBillDetailsRepository, SqlSP_AddUpdSBillDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSBillMasterRepository, SqlSP_AddUpdSBillMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSChallanDetailsRepository, SqlSP_AddUpdSChallanDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSChallanMasterRepository, SqlSP_AddUpdSChallanMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSOrderDetailsRepository, SqlSP_AddUpdSOrderDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSOrderMasterRepository, SqlSP_AddUpdSOrderMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSRequisitionDetailsRepository, SqlSP_AddUpdSRequisitionDetailsRepository>();

builder.Services.AddScoped<IGET_PChallanMasterRepository, SqlGET_PChallanMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSRequisitionMasterRepository, SqlSP_AddUpdSRequisitionMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSReturnDetailsRepository, SqlSP_AddUpdSReturnDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSReturnMasterRepository, SqlSP_AddUpdSReturnMasterRepository>();

builder.Services.AddScoped<IGET_SBillDetailsRepository, SqlGET_SBillDetailsRepository>();

builder.Services.AddScoped<IGET_SBillMasterRepository, SqlGET_SBillMasterRepository>();

builder.Services.AddScoped<IGET_SChallanDetailsRepository, SqlGET_SChallanDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSQuatationDetailsRepository, SqlSP_AddUpdSQuatationDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSQuatationMasterRepository, SqlSP_AddUpdSQuatationMasterRepository>();

builder.Services.AddScoped<IGET_SChallanMasterRepository, SqlGET_SChallanMasterRepository>();

builder.Services.AddScoped<IGET_PChallanSearchByIDRepository, SqlGET_PChallanSearchByIDRepository>();

builder.Services.AddScoped<IGET_SOrderDetailsRepository, SqlGET_SOrderDetailsRepository>();

builder.Services.AddScoped<IGET_SOrderMasterRepository, SqlGET_SOrderMasterRepository>();

builder.Services.AddScoped<IGET_SRequisitionDetailsRepository, SqlIGET_SRequisitionDetailsRepository>();

builder.Services.AddScoped<IGET_GoodReceiptNoteMasterRepository, SqlGET_GoodReceiptNoteMasterRepository>();

builder.Services.AddScoped<IGET_SRequisitionMasterRepository, SqlGET_SRequisitionMasterRepository>();

builder.Services.AddScoped<IGET_PChallanNoSearchRepository, SqlGET_PChallanNoSearchRepository>();

builder.Services.AddScoped<IGET_SQuatationDetailsRepository, SqlGET_SQuatationDetailsRepository>();

builder.Services.AddScoped<IGET_SQuatationMasterRepository, SqlGET_SQuatationMasterRepository>();

builder.Services.AddScoped<IGET_SReturnDetailsRepository, SqlGET_SReturnDetailsRepository>();

builder.Services.AddScoped<IGET_SReturnMasterRepository, SqlGET_SReturnMasterRepository>();

builder.Services.AddScoped<IGET_PBillMasterRepositroy, SqlGET_PBillMasterRepositroy>();

builder.Services.AddScoped<IGET_PBillNoSearchRepository, SqlGET_PBillNoSearchRepository>();

builder.Services.AddScoped<IGET_POrderNoSearchRepository, SqlGET_POrderNoSearchRepository>();

builder.Services.AddScoped<IGET_PReturnMasterRepository, SqlGET_PReturnMasterRepository>();

builder.Services.AddScoped<IGET_SBillNoSearchRepository, SqlGET_SBillNoSearchRepository>();

builder.Services.AddScoped<IGET_SChallanNoSearchRepository, SqlGET_SChallanNoSearchRepository>();

builder.Services.AddScoped<ISP_DeletePOrderRepository, SqlSP_DeletePOrderRepository>();

builder.Services.AddScoped<ISP_DeletePRequisitionRepository, SqlSP_DeletePRequisitionRepository>();

builder.Services.AddScoped<ISP_DeleteQuatationRepository, SqlSP_DeletePQuatationRepository>();

builder.Services.AddScoped<ISP_DeletePChallanRepository, SqlSP_DeletePChallanRepository>();

builder.Services.AddScoped<IGET_QuatationNoSearchRepository, SqlGET_SQuatationNoSearchRepository>();

builder.Services.AddScoped<IGET_SOrderNoSearchRepository, SqlGET_SOrderNoSearchRepository>();

builder.Services.AddScoped<ISP_DeletePBillRepository, SqlSP_DeletePBillRepository>();

builder.Services.AddScoped<IGET_PRequisitionDetailDataRepository, SqlGET_PRequisitionDetailDataRepository>();

builder.Services.AddScoped<ISP_DeletePRequisitionDetailsRepository, SqlSP_DeletePRequisitionDetailsRepository>();

builder.Services.AddScoped<ISP_MeassagePRequisitionRepository, SqlSP_MeassagePRequisitionRepository>();

builder.Services.AddScoped<IGET_QuatationDetailDataRepository, SqlGET_QuatationDetailDataRepository>();

builder.Services.AddScoped<IGET_SRequisitionNoSearchRepository, SqlGET_SRequisitionNoSearchRepository>();

builder.Services.AddScoped<IGET_QuatationSearchByIDRepository, SqlGET_QuatationSearchByIDRepository>();

builder.Services.AddScoped<ISP_MeassageQuatationRepository, SqlSP_MeassageQuatationRepository>();

builder.Services.AddScoped<IGET_POrderDetailsDataRepository, SqlGET_POrderDetailsDataRepository>();

builder.Services.AddScoped<IGET_BarCodeRepository, SqlGET_BarCodeRepository>();

builder.Services.AddScoped<ISP_AddBarCodeRepository, SqlSP_AddBarCodeRepository>();

builder.Services.AddScoped<ISP_MeassagePOrderRepository, SqlSP_MeassagePOrderRepository>();

builder.Services.AddScoped<IGET_PChallanDetailsByIdRepository, SqlGET_PChallanDetailsByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMDesignationRepository, SqlSP_AddUpdHRMDesignationRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMHolidayRepository, SqlSP_AddUpdHRMHolidayRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMEmployeesRepository, SqlSP_AddUpdHRMEmployeesRepository>();

builder.Services.AddScoped<IGET_SBillDetailsByIdRepository, SqlGET_SBillDetailsByIdRepository>();

builder.Services.AddScoped<IGET_SChallanDetailsByIdRepository, SqlGET_SChallanDetailsByIdRepository>();

builder.Services.AddScoped<IGET_SRequisitionDetailByIdRepository, SqlGET_SRequisitionDetailByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMEmpolyeeLeavesRepository, SqlSP_AddUpdHRMEmpolyeeLeavesRepository>();

builder.Services.AddScoped<IGET_SReturnDetailsByIdRepository, SqlGET_SReturnDetailsByIdRepository>();

builder.Services.AddScoped<IGET_PBillDetailsDataRepository, SqlGET_PBillDetailsDataRepository>();

builder.Services.AddScoped<ISP_MeassagePChallanRepository, SqlSP_MeassagePChallanRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMLeaveTypeRepository, SqlSP_AddUpdHRMLeaveTypeRepository>();

builder.Services.AddScoped<IGET_HRM_Empoye_LeavesRepository, SqlGET_HRM_Empoye_LeavesRepository>();

builder.Services.AddScoped<IGET_HRM_HolidayRepository, SqlGET_HRM_HolidayRepository>();

builder.Services.AddScoped<IGET_SQuotationDetailByIdRepository, SqlGET_SQuotationDetailByIdRepository>();

builder.Services.AddScoped<IGET_GoodReceiptNoteDataByIdRepository, SqlGET_GoodReceiptNoteDataByIdRepository>();

builder.Services.AddScoped<IGET_HRMDesignationNameRepository, SqlGET_HRMDesignationNameRepository>();

builder.Services.AddScoped<IGET_GoodReceiptNoteAssuranceByIdRepository, SqlGET_GoodReceiptNoteAssuranceByIdRepository>();

builder.Services.AddScoped<IGET_HRMShiftInfoNameRepository, SqlGET_HRMShiftInfoNameRepository>();

builder.Services.AddScoped<IGET_CompanyMasterRepository, SqlGET_CompanyMasterRepository>();

builder.Services.AddScoped<ISP_DeleteCompanyMasterRepository, SqlSP_DeleteCompanyMasterRepository>();

builder.Services.AddScoped<ISP_DeleteSBillRepository, SqlSP_DeleteSBillRepository>();

builder.Services.AddScoped<ISP_DeleteSChallanRepository, SqlSP_DeleteSChallanRepository>();

builder.Services.AddScoped<ISP_DeleteSQuatationRepository, SqlSP_DeleteSQuatationRepository>();

builder.Services.AddScoped<ISP_DeleteSRequisitionRepository, SqlSP_DeleteSRequisitionRepository>();

builder.Services.AddScoped<ISP_DeleteSReturnRepository, SqlSP_DeleteSReturnRepository>();

builder.Services.AddScoped<IGET_LeaveTypeRepository, SqlGET_LeaveTypeRepository>();

builder.Services.AddScoped<IGET_DepartmentNameRepository, SqlGET_DepartmentNameRepository>();

builder.Services.AddScoped<IGET_PReturnDataByIDRepository, SqlGET_PReturnDataByIDRepository>();

builder.Services.AddScoped<IGET_HRMDesignationRepository, SqlGET_HRMDesignationRepository>();

builder.Services.AddScoped<ISP_DeleteLeaveTypeRepository, SqlSP_DeleteLeaveTypeRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMShiftInfoRepository, SqlSP_AddUpdHRMShiftInfoRepository>();

builder.Services.AddScoped<IGET_HRMShiftInfoRepository, SqlGET_HRMShiftInfoRepository>();

builder.Services.AddScoped<ISP_AddUpdPayStructureRepository, SqlSP_AddUpdPayStructureRepository>();

builder.Services.AddScoped<IGET_PayStructureRepository, SqlGET_PayStructureRepository>();

builder.Services.AddScoped<IGET_SQuatationDataByIDRepository, SqlGET_SQuatationDataByIDRepository>();

builder.Services.AddScoped<IGET_SRequisitionDataByIDRepository, SqlGET_SRequisitionDataByIDRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMPayrollRepository, SqlSP_AddUpdHRMPayrollRepository>();

builder.Services.AddScoped<IGET_HRMPayrollRepository, SqlGET_HRMPayrollRepository>();

builder.Services.AddScoped<IGET_SChallanDataByIRepository, SqlGET_SChallanDataByIRepository>();

builder.Services.AddScoped<ISP_AddUpdExpenseRepository, SqlSP_AddUpdExpenseRepository>();

builder.Services.AddScoped<IGET_ExpenseRepository, SqlGET_ExpenseRepository>();

builder.Services.AddScoped<ISP_AddUpdExpenseCategoryRepository, SqlSP_AddUpdExpenseCategoryRepository>();

builder.Services.AddScoped<IGET_ExpenseCategoryRepository, SqlGET_ExpenseCategoryRepository>();

builder.Services.AddScoped<ISP_DeleteHRMDesignationRepository, SqlSP_DeleteHRMDesignationRepository>();

builder.Services.AddScoped<ISP_DeleteHRMHolidayRepository, SqlSP_DeleteHRMHolidayRepository>();

builder.Services.AddScoped<ISP_DeleteHRMShiftInfoRepository, SqlSP_DeleteHRMShiftInfoRepository>();

builder.Services.AddScoped<ISP_DeletePayStructureRepository, SqlSP_DeletePayStructureRepository>();

builder.Services.AddScoped<IGET_EmployeeLeavesDateRepository, SqlGET_EmployeeLeavesDateRepository>();

builder.Services.AddScoped<IGET_HRMEmployeesNameRepository, SqlGET_HRMEmployeesNameRepository>();

builder.Services.AddScoped<ISP_DeleteEmployeeLeavesRepository, SqlSP_DeleteEmployeeLeavesRepository>();

builder.Services.AddScoped<IGET_HRMEmployeesRepository, SqlGET_HRMEmployeesRepository>();

builder.Services.AddScoped<IGET_CategoryDropdownRepository, SqlGET_CategoryDropdownRepository>();

builder.Services.AddScoped<IGET_SubCategoryDropdownRepository, SqlGET_SubCategoryDropdownRepository>();

builder.Services.AddScoped<IGET_EMPLOYEERepository, SqlGET_EMPLOYEERepository>();

builder.Services.AddScoped<IGET_CompanyDropdownRepository, SqlGET_CompanyDropdownRepository>();

builder.Services.AddScoped<IGET_DesignationDropdownRepository, SqlGET_DesignationDropdownRepository>();

builder.Services.AddScoped<ISP_DeleteExpensesRepository, SqlSP_DeleteExpensesRepository>();

builder.Services.AddScoped<ISP_DeleteExpensesCategoryRepository, SqlSP_DeleteExpensesCategoryRepository>();

builder.Services.AddScoped<ISP_DeleteHRMEmployeesRepository, SqlSP_DeleteHRMEmployeesRepository>();

builder.Services.AddScoped<IGET_ACCSESSRIGHTDropdownRepository, SqlGET_ACCSESSRIGHTDropdownRepository>();

builder.Services.AddScoped<IServiceMasterRepository, SqlIServiceMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdCategoryMasterRepository, SqlSP_AddUpdCategoryMasterRepository>();

builder.Services.AddScoped<IGET_CategoryMasterDataRepository, SqlGET_CategoryMasterDataRepository>();

builder.Services.AddScoped<ISP_DeleteCategoryRepository, SqlSP_DeleteCategoryRepository>();

builder.Services.AddScoped<IGET_SubCategoryMasterRepository, SqlGET_SubCategoryMasterRepository>();

builder.Services.AddScoped<ISP_DeleteCategoryMasterRepository, SqlSP_DeleteCategoryMasterRepository>();

builder.Services.AddScoped<ISP_DeleteHRMPayrollRepository, SqlSP_DeleteHRMPayrollRepository>();

builder.Services.AddScoped<IGET_UOMImplicationRepository, SqlGET_UOMImplicationRepository>();

builder.Services.AddScoped<ISP_AddUpdHRMEmpolyeeLeavesStatusRepository, SqlSP_AddUpdHRMEmpolyeeLeavesStatusRepository>();

builder.Services.AddScoped<ISP_UpdateCustomerDetailsRepository, SqlSP_UpdateCustomerDetailsRepository>();

builder.Services.AddScoped<IGET_UnitMasterDataRepository, SqlGET_UnitMasterDataRepository>();

builder.Services.AddScoped<ISP_AddUpdWarrrantyRepository, SqlSP_AddUpdWarrrantyRepository>();

builder.Services.AddScoped<IGET_WarrantyRepository, SqlGET_WarrantyRepository>();

builder.Services.AddScoped<ISP_DeleteWarrantyRepository, SqlSP_DeleteWarrantyRepository>();

builder.Services.AddScoped<ISP_AddUpdStockTransferDRepository, SqlSP_AddUpdStockTransferDRepository>();

builder.Services.AddScoped<ISP_AddUpdStockTransferMRepository, SqlSP_AddUpdStockTransferMRepository>();

builder.Services.AddScoped<ISP_AddUpdPDeficitStockMasterRepository, SqlSP_AddUpdPDeficitStockMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdPDeficitDetailsRepository, SqlSP_AddUpdPDeficitDetailsRepository>();

builder.Services.AddScoped<IGET_DeficitRepository, SqlGET_DeficitRepository>();

builder.Services.AddScoped<ISP_DeleteDeficitRepository, SqlSP_DeleteDeficitRepository>();

builder.Services.AddScoped<IGET_DeficitByIdRepository, SqlGET_DeficitByIdRepository>();

builder.Services.AddScoped<IGET_CounterNameRepository, SqlGET_CounterNameRepository>();

builder.Services.AddScoped<ISP_DeleteStockTransferRepository, SqlSP_DeleteStockTransferRepository>();

builder.Services.AddScoped<IGET_BILLCOUNTSRepository, SqlGET_BILLCOUNTSRepository>();

builder.Services.AddScoped<IGET_StockTransferRepository, SqlGET_StockTransferRepository>();

builder.Services.AddScoped<IGET_StockTransferDRepository, SqlGET_StockTransferDRepository>();

builder.Services.AddScoped<IGET_StockCountRepository, SqlGET_StockCountRepository>();

builder.Services.AddScoped<ISP_AddUpdServiceChargeRepository, SqlSP_AddUpdServiceChargeRepository>();

builder.Services.AddScoped<IGET_ServiceChargeRepository, SqlGET_ServiceChargeRepository>();

builder.Services.AddScoped<ISP_DeleteServiceChargeRepository, SqlSP_DeleteServiceChargeRepository>();

builder.Services.AddScoped<ISP_AddUpdTransportersRepository, SqlSP_AddUpdTransportersRepository>();

builder.Services.AddScoped<ISP_AddUpdFarmerRepository, SqlSP_AddUpdFarmerRepository>();

builder.Services.AddScoped<ISP_AddUpdGateEntryRepository, SqlSP_AddUpdGateEntryRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariMasterRepository, SqlSP_AddUpdVyapariMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdPlotMasterRepository, SqlSP_AddUpdPlotMasterRepository>();

builder.Services.AddScoped<IGET_GateEntryRepository, SqlGET_GateEntryRepository>();

builder.Services.AddScoped<IGET_TransportersRepository, SqlGET_TransportersRepository>();

builder.Services.AddScoped<IGET_VyapariMasterRepository, SqlGET_VyapariMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdAuctionShedDeatilRepository, SqlSP_AddUpdAuctionShedDeatilRepository>();

builder.Services.AddScoped<ISP_AddUpdAuctionshedMasterRepository, SqlSP_AddUpdAuctionshedMasterRepository>();

builder.Services.AddScoped<IGET_GateFarmerRepository, SqlGET_GateFarmerRepository>();

builder.Services.AddScoped<IGET_PlotMasterRepository, SqlGET_PlotMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdRECEIPTMasterRepository, SqlSP_AddUpdRECEIPTMasterRepository>();

builder.Services.AddScoped<ISP_DeleteFarmerRepositorycs, SqlSP_DeleteFarmerRepositorycs>();

builder.Services.AddScoped<ISP_DeleteGateEntryRepository, SqlSP_DeleteGateEntryRepository>();

builder.Services.AddScoped<ISP_DeletePlotMasterRepository, SqlSP_DeletePlotMasterRepository>();

builder.Services.AddScoped<ISP_DeleteTransporterRepository, SqlSP_DeleteTransporterRepository>();

builder.Services.AddScoped<ISP_DeleteAuctionshedRepository, SqlSP_DeleteAuctionshedRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariMasterRepository, SqlSP_DeleteVyapariMasterRepository>();

builder.Services.AddScoped<IGET_AUCTIONSHEDRepository, SqlGET_AUCTIONSHEDRepository>();

builder.Services.AddScoped<ISP_AddFamilyDetailsRepository, SqlSP_AddFamilyDetailsRepository>();

builder.Services.AddScoped<IGET_TOKENNoSearchRepository, SqlGET_TOKENNoSearchRepository>();

builder.Services.AddScoped<IGET_AuctionDetailsByIdRepository, SqlGET_AuctionDetailsByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdST_ImplicationRepository, SqlSP_AddUpdST_ImplicationRepository>();

builder.Services.AddScoped<IGET_ST_Implication_ServiceChargeRepository, SqlGET_ST_Implication_ServiceChargeRepository>();

builder.Services.AddScoped<IGET_FamilyDetailsByIdRepository, SqlGET_FamilyDetailsByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdRECEIPTDetailsRepository, SqlSP_AddUpdRECEIPTDetailsRepository>();

builder.Services.AddScoped<IGETNewGenrateTokenNoRepository, SqlGETNewGenrateTokenNoRepository>();

builder.Services.AddScoped<IGETItemDropdownRepository, SqlGETItemDropdownRepository>();

builder.Services.AddScoped<ISP_AddUpdMarketStoreRepository, SqlSP_AddUpdMarketStoreRepository>();

builder.Services.AddScoped<IGET_VaypariNameRepository, SqlGET_VaypariNameRepository>();

builder.Services.AddScoped<IGET_MarketStoreRepository, SqlGET_MarketStoreRepository>();

builder.Services.AddScoped<ISP_DeleteMarketStoreRepository, SqlSP_DeleteMarketStoreRepository>();

builder.Services.AddScoped<IGET_RECEIPTMasterRepository, SqlGET_RECEIPTMasterRepository>();

builder.Services.AddScoped<IGET_FARMERSERVICERepository, SqlGET_FARMERSERVICERepository>();

builder.Services.AddScoped<ISP_AddUpdFarmerFamilyRepository, SqlSP_AddUpdFarmerFamilyRepository>();

builder.Services.AddScoped<ISP_AddUpdGateEntryDetailRepository, SqlSP_AddUpdGateEntryDetailRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailByIdRepository, SqlGET_GateEntryDetailByIdRepository>();

builder.Services.AddScoped<IGET_AuctionDataBillRepository, SqlGET_AuctionDataBillRepository>();

builder.Services.AddScoped<IGET_MainGroupRepository, SqlGET_MainGroupRepository>();

builder.Services.AddScoped<IGET_SubgroupRepository, SqlGET_SubgroupRepository>();

builder.Services.AddScoped<IGET_GeneralGruopRepository, SqlGET_GeneralGruopRepository>();

builder.Services.AddScoped<IGET_ADHAARAPPRepository, SqlGET_ADHAARAPPRepository>();

builder.Services.AddScoped<ISP_AddUpdFarmerAppRepository, SqlSP_AddUpdFarmerAppRepository>();

builder.Services.AddScoped<ISP_AddUpdAccountsRepository, SqlSP_AddUpdAccountsRepository>();

builder.Services.AddScoped<IGET_ACCOUNTRepository, SqlGET_ACCOUNTRepository>();

builder.Services.AddScoped<ISP_AddUpdFarmerAppRepository, SqlSP_AddUpdFarmerAppRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariApprovalRepository, SqlSP_AddUpdVyapariApprovalRepository>();

builder.Services.AddScoped<ISP_DeleteAccountsRepository, SqlSP_DeleteAccountsRepository>();

builder.Services.AddScoped<IGET_RokdaVyapariMasterRepository, SqlGET_RokdaVyapariMasterRepository>();

builder.Services.AddScoped<IGET_ST_Implication_ShopLOCRepository, SqlGET_ST_Implication_ShopLOCRepository>();

builder.Services.AddScoped<ISP_AddUpdShopAlotRepository, SqlSP_AddUpdShopAlotRepository>();

builder.Services.AddScoped<IGET_ShopAlotRepository, SqlGET_ShopAlotRepository>();

builder.Services.AddScoped<ISP_DeleteShopAlotRepository, SqlSP_DeleteShopAlotRepository>();

builder.Services.AddScoped<IGET_FarmerNameRepository, SqlGET_FarmerNameRepository>();

builder.Services.AddScoped<IGET_FarmerCropsRepository, SqlGET_FarmerCropsRepository>();

builder.Services.AddScoped<ISP_DeleteFamilyDetailsRepository, SqlSP_DeleteFamilyDetailsRepository>();

builder.Services.AddScoped<IGET_ShopNameRepository, SqlGET_ShopNameRepository>();

builder.Services.AddScoped<IGET_AuctionDataSearchRepository, SqlGET_AuctionDataSearchRepository>();

builder.Services.AddScoped<ISP_CheckAadhaarRepository, SqlSP_CheckAadhaarRepository>();

builder.Services.AddScoped<ISP_DeleteRECEIPTMasterRepository, SqlSP_DeleteRECEIPTMasterRepository>();

builder.Services.AddScoped<ISP_CheckVehicalNoRepository, SqlSP_CheckVehicalNoRepository>();

builder.Services.AddScoped<IGET_FarmerSearchRepository, SqlGET_FarmerSearchRepository>();

builder.Services.AddScoped<IGET_ReceiptDetailsByIdRepository, SqlGET_ReceiptDetailsByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdPlotDetailsDtoRepository, SqlSP_AddUpdPlotDetailsDtoRepository>();

builder.Services.AddScoped<ISP_CheckVyapariAadhaarRepository, SqlSP_CheckVyapariAadhaarRepository>();

builder.Services.AddScoped<IGET_StandingI_RateA_FarmerRepository, SqlGET_StandingI_RateA_FarmerRepository>();

builder.Services.AddScoped<IGET_StandingI_RateA_VyapariRepository, SqlGET_StandingI_RateA_VyapariRepository>();

builder.Services.AddScoped<ISP_AddVehicalDetailsRepository, SqlSP_AddVehicalDetailsRepository>();

builder.Services.AddScoped<IGET_StandingI_RateA_ShopAlotRepository, SqlGET_StandingI_RateA_ShopAlotRepository>();

builder.Services.AddScoped<IGET_VehicalDetailsByIdRepository, SqlGET_VehicalDetailsByIdRepository>();

builder.Services.AddScoped<IGET_ServiceChargesByIdRepository, SqlGET_ServiceChargesByIdRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariLimitRepository, SqlSP_AddUpdVyapariLimitRepository>();

builder.Services.AddScoped<IGET_PlotDetailsByIdRepository, SqlGET_PlotDetailsByIdRepository>();

builder.Services.AddScoped<ISP_DeletePlotDetailsRepository, SqlSP_DeletePlotDetailsRepository>();

builder.Services.AddScoped<IGET_DistrictRepository, SqlGET_DistrictRepository>();

builder.Services.AddScoped<IGETNewGenrateBillNoRepository, SqlGETNewGenrateBillNoRepository>();

builder.Services.AddScoped<ISP_AddFamilyDetailsAppRepository, SqlSP_AddFamilyDetailsAppRepository>();

builder.Services.AddScoped<IGET_VyapariLimitRepository, SqlGET_VyapariLimitRepository>();

builder.Services.AddScoped<IGET_VyapariApprovalListRepository, SqlGET_VyapariApprovalListRepository>();

builder.Services.AddScoped<ISP_AddUpdStandingInstrcutionRepository, SqlSP_AddUpdStandingInstrcutionRepository>();

builder.Services.AddScoped<IGET_NewTokenRepository, SqlGET_NewTokenRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailAllAppRepository, SqlGET_GateEntryDetailAllAppRepository>();

builder.Services.AddScoped<IGET_VyapariApprovalByIDRepository, SqlGET_VyapariApprovalByIDRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariSanctionRepository, SqlSP_AddUpdVyapariSanctionRepository>();

builder.Services.AddScoped<IGET_AUCTIONSHEDdataRepository, SqlGET_AUCTIONSHEDdataRepository>();

builder.Services.AddScoped<IGET_VaypariNameIDRepository, SqlGET_VaypariNameIDRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailListRepository, SqlGET_GateEntryDetailListRepository>();

builder.Services.AddScoped<ISP_DeleteGateEntryDetailRepository, SqlSP_DeleteGateEntryDetailRepository>();

builder.Services.AddScoped<ISP_DeleteGateEntryTokenRepository, SqlSP_DeleteGateEntryTokenRepository>();

builder.Services.AddScoped<IGET_VyapariSanctionRepository, SqlGET_VyapariSanctionRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailRepository, SqlGET_GateEntryDetailRepository>();

builder.Services.AddScoped<IGET_FarmerFromFamilyDetailsRepository, SqlGET_FarmerFromFamilyDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdBankDetailsRepository, SqlSP_AddUpdBankDetailsRepository>();

builder.Services.AddScoped<IGET_GateBankDetailsRepository, SqlGET_GateBankDetailsRepository>();

builder.Services.AddScoped<IGET_VyapariDropdownRepository, SqlGET_VyapariDropdownRepository>();

builder.Services.AddScoped<ISP_DeleteBankDetailsRepository, SqlSP_DeleteBankDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariBillMasterRepository, SqlSP_AddUpdVyapariBillMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariBillDetailRepository, SqlSP_AddUpdVyapariBillDetailRepository>();

builder.Services.AddScoped<IProfileUploadRepository, SqlProfileUploadRepository>();

builder.Services.AddScoped<IGET_GenrateTokenNORepository, SqlGET_GenrateTokenNORepository>();

builder.Services.AddScoped<IGET_BankNameRepository, SqlGET_BankNameRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariLimitDetailsRepository, SqlSP_DeleteVyapariLimitDetailsRepository>();

builder.Services.AddScoped<ISP_AddCashInvertRepository, SqlSP_AddCashInvertRepository>();

builder.Services.AddScoped<ISP_DeleteVyaparisanctionRepository, SqlSP_DeleteVyaparisanctionRepository>();

builder.Services.AddScoped<ISP_AddUpdGateEntryDetailTOKENRRepository, SqlSP_AddUpdGateEntryDetailTOKENRRepository>();

builder.Services.AddScoped<EmailService>();

builder.Services.AddScoped<IGET_RECEIPTDataBillRepository, SqlGET_RECEIPTDataBillRepository>();

builder.Services.AddScoped<IGET_VYAPARISERVICERepository, SqlGET_VYAPARISERVICERepository>();

builder.Services.AddScoped<IGET_GateCashInvertRepository, SqlGET_GateCashInvertRepository>();

builder.Services.AddScoped<ISP_DeleteCashInvertRepository, SqlSP_DeleteCashInvertRepository>();

builder.Services.AddScoped<ISP_AddUpdNotificationRepository, SqlSP_AddUpdNotificationRepository>();

builder.Services.AddScoped<IGET_GenrateACCNORepository, SqlGET_GenrateACCNORepository>();

builder.Services.AddScoped<ISP_AddUpdRECEIPTMasterAddAuctionRepository, SqlSP_AddUpdRECEIPTMasterAddAuctionRepository>();

builder.Services.AddScoped<IGET_NotificationRepository, SqlGET_NotificationRepository>();

builder.Services.AddScoped<ISP_DeleteNotificationRepository, SqlSP_DeleteNotificationRepository>();

builder.Services.AddScoped<IGET_CheckMobileAppRepository, SqlGET_CheckMobileAppRepository>();

builder.Services.AddScoped<IGET_DashCountRepository, SqlGET_DashCountRepository>();

builder.Services.AddScoped<IGET_StandingInstrcutionRepository, SqlGET_StandingInstrcutionRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailVehnoRepository, SqlGET_GateEntryDetailVehnoRepository>();

builder.Services.AddScoped<IISP_AddVehicalDetailsAppRepositor, SqlSP_AddVehicalDetailsAppRepositor>();

builder.Services.AddScoped<ISP_DeleteVehicaldetailsRepository, SqlSP_DeleteVehicaldetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdAdminVideoRepository, SqlSP_AddUpdAdminVideoRepository>();

builder.Services.AddScoped<ISP_AddUpdAdvertisementRepository, SqlSP_AddUpdAdvertisementRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariBillMasterSaveAuctionRepository, SqlSP_AddUpdVyapariBillMasterSaveAuctionRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariApproveFarmerRepository, SqlSP_AddUpdVyapariApproveFarmerRepository>();

builder.Services.AddScoped<ISP_AddUpdCashSummaryRepository, SqlSP_AddUpdCashSummaryRepository>();

builder.Services.AddScoped<IGET_AccountACCTMRepository, SqlGET_AccountACCTMRepository>();

builder.Services.AddScoped<IGET_AdminVideoRepository, SqlGET_AdminVideoRepository>();

builder.Services.AddScoped<IGET_AdvertisementRepository, SqlGET_AdvertisementRepository>();

builder.Services.AddScoped<IGET_RECEIPTDataBillByTokenRepository, SqlGET_RECEIPTDataBillByTokenRepository>();

builder.Services.AddScoped<IGET_VapariStandingInstrcutionRepository, SqlGET_VapariStandingInstrcutionRepository>();

builder.Services.AddScoped<IGET_GateCashSummaryRepository, SqlGET_GateCashSummaryRepository>();

builder.Services.AddScoped<ISP_AddUpdVoucherMasterRepository, SqlSP_AddUpdVoucherMasterRepository>();

builder.Services.AddScoped<ISP_DeleteAdminVideoRepository, SqlSP_DeleteAdminVideoRepository>();

builder.Services.AddScoped<ISP_DeleteAdvertisementRepository, SqlSP_DeleteAdvertisementRepository>();

builder.Services.AddScoped<IGET_VoucherMasterRepository, SqlGET_VoucherMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdAuctionShedDeatilByBillRepository, SqlSP_AddUpdAuctionShedDeatilByBillRepository>();

builder.Services.AddScoped<ISP_InsertVyapariDueDataRepository, SqlSP_InsertVyapariDueDataRepository>();

builder.Services.AddScoped<ISP_AddUpdAuctionshedMasterByBillRepository, SqlSP_AddUpdAuctionshedMasterByBillRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariBillDetailByBillRepository, SqlSP_AddUpdVyapariBillDetailByBillRepository>();

builder.Services.AddScoped<IGET_FarmerAddedByTranspoterRepository, SqlGET_FarmerAddedByTranspoterRepository>();

builder.Services.AddScoped<IGET_FarmerAddedByTranspoterAllRepository, SqlGET_FarmerAddedByTranspoterAllRepository>();

builder.Services.AddScoped<IISP_AddUpdVyapariBillMasterByBillRepository, SqlSP_AddUpdVyapariBillMasterByBillRepository>();

builder.Services.AddScoped<IGET_AUCTIONSHEDVAPARIRepository, SqlGET_AUCTIONSHEDVAPARIRepository>();

builder.Services.AddScoped<IGET_RECEIPTMasterAUCTIONRepository, SqlGET_RECEIPTMasterAUCTIONRepository>();

builder.Services.AddScoped<ISP_DeleteRECEIPTMasterAuctionRepository, SqlSP_DeleteRECEIPTMasterAuctionRepository>();

builder.Services.AddScoped<IGET_VoucherMasterJamaRepository, SqlGET_VoucherMasterJamaRepository>();

builder.Services.AddScoped<IGET_VoucherMasterNaveRepository, SqlGET_VoucherMasterNaveRepository>();

builder.Services.AddScoped<ISP_DeleteVoucherMasterRepository, SqlSP_DeleteVoucherMasterRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariMasterAuctionRepository, SqlSP_DeleteVyapariMasterAuctionRepository>();

builder.Services.AddScoped<IVideoUploadRepository, SQLVideoUploadRepository>();

builder.Services.AddScoped<ISP_AddUpdCashCounterRepository, SqlSP_AddUpdCashCounterRepository>();

builder.Services.AddScoped<ISP_DeleteCashCounterRepository, SqlSP_DeleteCashCounterRepository>();

builder.Services.AddScoped<IGET_CashCounterRepository, SqlGET_CashCounterRepository>();

builder.Services.AddScoped<IGET_CreateBillRepository, SqlGET_CreateBillRepository>();

builder.Services.AddScoped<IGET_BillNoSearchRepository, SqlGET_BillNoSearchRepository>();

builder.Services.AddScoped<IGET_FarmerSearchBYAadhaarRepository, SqlGET_FarmerSearchBYAadhaarRepository>();

builder.Services.AddScoped<ISP_AddUpdCropItemRepository, SqlSP_AddUpdCropItemRepository>();

builder.Services.AddScoped<IGET_FarmerBillChargesdRepository, SqlGET_FarmerBillChargesdRepository>();

builder.Services.AddScoped<IGET_CropItemRepository, SqlGET_CropItemRepository>();

builder.Services.AddScoped<ISP_AddVoucherRepository, SqlSP_AddVoucherRepository>();

builder.Services.AddScoped<IGET_VoucherRepository, SqlGET_VoucherRepository>();

builder.Services.AddScoped<ISP_DeleteCropItemDetailsRepository, SqlSP_DeleteCropItemDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdUserMastersRepository, SqlSP_AddUpdUserMastersRepository>();

builder.Services.AddScoped<ISP_UpadateUserLoginRepository, SqlSP_UpadateUserLoginRepository>();

builder.Services.AddScoped<IGET_VyapariAppRepository, SqlGET_VyapariAppRepository>();

builder.Services.AddScoped<ISP_AddUpdGateEntryDetailISCOMPLETEDRepository, SqlSP_AddUpdGateEntryDetailISCOMPLETEDRepository>();

builder.Services.AddScoped<IGET_VYAPARIDUEDATARepository, SqlGET_VYAPARIDUEDATARepository>();

builder.Services.AddScoped<IGET_FARMERRepository, SqlGET_FARMERRepository>();

builder.Services.AddScoped<IGET_FARMERBILLRepository, SqlGET_FARMERBILLRepository>();

builder.Services.AddScoped<IGET_CashDetailsRepository, SqlGET_CashDetailsRepository>();

builder.Services.AddScoped<IGET_CreateBill_VyapariRepository, SqlGET_CreateBill_VyapariRepository>();

builder.Services.AddScoped<IGET_VyapariBillChargesRepository, SqlGET_VyapariBillChargesRepository>();

builder.Services.AddScoped<IGET_VyapariAppFarmerRepository, SqlGET_VyapariAppFarmerRepository>();

builder.Services.AddScoped<IUpdateHeartbeatRepository, SqlUpdateHeartbeatRepository>();

builder.Services.AddScoped<IGET_VyapariBillMasterDataRepository, SqlGET_VyapariBillMasterDataRepository>();

builder.Services.AddScoped<ISP_AddUpdGateEntryDetailAppRepository, SqlSP_AddUpdGateEntryDetailAppRepository>();

builder.Services.AddScoped<IGET_VyapariBillMasterRepository, SqlGET_VyapariBillMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdCashSummaryUpdateRepository, SqlSP_AddUpdCashSummaryUpdateRepository>();

builder.Services.AddScoped<IGET_CheckUserSessionRepository, SqlGET_CheckUserSessionRepository>();

builder.Services.AddScoped<IPdfUploadRepository, SqlPdfUploadRepository>();

builder.Services.AddScoped<ISP_DeleteSFarmerRepository, SqlSP_DeleteSFarmerRepository>();

builder.Services.AddScoped<IGET_FBILLSERVICESRepository, SqlGET_FBILLSERVICESRepository>();

builder.Services.AddScoped<ISP_DeleteVoucherRepository, SqlSP_DeleteVoucherRepository>();

builder.Services.AddScoped<IGET_VyapariBillMasterAuctionRepository, SqlGET_VyapariBillMasterAuctionRepository>();

builder.Services.AddScoped<IGetAdminAdvertisementRepository, SqlGetAdminAdvertisementRepository>();

builder.Services.AddScoped<IGetAdminVideoAppRepository, SqlGetAdminVideoAppRepository>();

builder.Services.AddScoped<IGetItemImageRepository, SqlGetItemImageRepository>();

builder.Services.AddScoped<IGET_RECEIPTMasterCashCounterRepositorycs, SqlGET_RECEIPTMasterCashCounterRepository>();

builder.Services.AddScoped<IGET_GateEntryDataByVehnoRepository, SqlGET_GateEntryDataByVehnoRepository>();

builder.Services.AddScoped<IGET_Report_VyapariRepository, SqlGET_Report_VyapariRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariBillDetailAuctionRepository, SqlSP_AddUpdVyapariBillDetailAuctionRepository>();

builder.Services.AddScoped<IGET_Report_farmerRepository, SqlGET_Report_farmerRepository>();

builder.Services.AddScoped<IGET_VyapariLoginRepository, SqlGET_VyapariLoginRepository>();

builder.Services.AddScoped<IGET_Report_AuctionRepository, SqlGET_Report_AuctionRepository>();

builder.Services.AddScoped<ISP_VyapariEmailCheakRepository, SqlSP_VyapariEmailCheakRepository>();

builder.Services.AddScoped<IGET_CashCounterMatchDataRepository, SqlGET_CashCounterMatchDataRepository>();

builder.Services.AddScoped<IGET_ActiveVyapariDetailsRepository, SqlGET_ActiveVyapariDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdChequeCounterUpdateRepository, SqlSP_AddUpdChequeCounterUpdateRepository>();

builder.Services.AddScoped<IGET_RECEIPTMasterDataMatchRepository, SqlGET_RECEIPTMasterDataMatchRepository>();

builder.Services.AddScoped<IGET_FarmerDetailsRepository, SqlGET_FarmerDetailsRepository>();

builder.Services.AddScoped<IGET_AuctionDataByTokenRepository, SqlGET_AuctionDataByTokenRepository>();

builder.Services.AddScoped<IGET_VoucherNaveRepository, SqlGET_VoucherNaveRepository>();

builder.Services.AddScoped<IGET_VoucherJamaRepository, SqlGET_VoucherJamaRepository>();

builder.Services.AddScoped<ISP_AddUpdColdStorageInwardRepository, SqlSP_AddUpdColdStorageRepository>();

builder.Services.AddScoped<IGET_ColdStorageInwardRepository, SqlGET_ColdStorageRepository>();

builder.Services.AddScoped<ISP_DeleteColdStorageDetailsRepository, SqlSP_DeleteColdStorageDetailsRepository>();

builder.Services.AddScoped<ISP_AddVoucherByIDRepository, SqlSP_AddVoucherByIDRepository>();

builder.Services.AddScoped<ISP_AddUpdPreCoolingRepository, SqlSP_AddUpdPreCoolingRepository>();

builder.Services.AddScoped<ISP_AddUpdCaretRepository, SqlSP_AddUpdCaretRepository>();

builder.Services.AddScoped<IGET_PreCoolingRepository, SqlGET_PreCoolingRepository>();

builder.Services.AddScoped<IGET_CaretRepository, SqlGET_CaretRepository>();

builder.Services.AddScoped<ISP_DeleteCaretRepository, SqlSP_DeleteCaretRepository>();

builder.Services.AddScoped<ISP_DeletePreCoolingRepository, SqlSP_DeletePreCoolingRepository>();

builder.Services.AddScoped<IGET_VoucherJamaNaveRepository, SqlGET_VoucherJamaNaveRepository>();


builder.Services.AddScoped<IGET_ST_Implication_DayRepository, SqlGET_ST_Implication_DayRepository>();

builder.Services.AddScoped<ISP_FarmerBillVoucherRepository, SqlSP_FarmerBillVoucherRepository>();

builder.Services.AddScoped<ISP_AddUpdComplaintRepository, SqlSP_AddUpdComplaintRepository>();

builder.Services.AddScoped<ISP_DeleteComplaintRepository, SqlSP_DeleteComplaintRepository>();

builder.Services.AddScoped<IGET_ComplaintRepository, SqlGET_ComplaintRepository>();

builder.Services.AddScoped<ISP_AddUpdRewardPointsRepository, SqlSP_AddUpdRewardPointsRepository>();

builder.Services.AddScoped<ISP_AddUpdColdStorageOutwardRepository, SqlSP_AddUpdColdStorageOutwardRepository>();

builder.Services.AddScoped<IGET_CountPrecoolingRepository, SqlGET_CountPrecoolingRepository>();

builder.Services.AddScoped<IGET_RewardPointRepository, SqlGET_RewardPointRepository>();

builder.Services.AddScoped<ISP_DeleteRewardRepository, SqlSP_DeleteRewardRepository>();

builder.Services.AddScoped<IGET_AdvertisementAppRepository, SqlGET_AdvertisementAppRepository>();

builder.Services.AddScoped<IGet_AdminVideoAppRepository, SqlGet_AdminVideoAppRepository>();

builder.Services.AddScoped<ISP_VyapariBillVoucherRepository, SqlSP_VyapariBillVoucherRepository>();

builder.Services.AddScoped<IGET_VyapariApproveRepository, SqlGET_VyapariApproveRepository>();

builder.Services.AddScoped<IDocumentUploadRepository, SqlDocumentUploadRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariApproveRepository, SqlSP_AddUpdVyapariApproveRepository>();

builder.Services.AddScoped<ISP_UpdFarmerProfileRepository, SqlSP_UpdFarmerProfileRepository>();

builder.Services.AddScoped<ISP_AddUpdCompanyInfoRepository, SqlSP_AddUpdCompanyInfoRepository>();

builder.Services.AddScoped<IGET_CompanyInfoRepository, SqlGET_CompanyInfoRepository>();

builder.Services.AddScoped<ISP_DeleteCompanyInfoRepository, SqlSP_DeleteCompanyInfoRepository>();

builder.Services.AddScoped<ISP_UpdTranspoterProfileRepository, SqlSP_UpdTranspoterProfileRepository>();

builder.Services.AddScoped<ISP_AddUpdOTPRepository, SqlSP_AddUpdOTPRepository>();

builder.Services.AddScoped<IGET_OTPRepository, SqlGET_OTPRepository>();

builder.Services.AddScoped<IGET_FARMERAPPBillRepository, SqlGET_FARMERAPPBillRepository>();

builder.Services.AddScoped<ISP_CheckOTPRepository, SqlSP_CheckOTPRepository>();

builder.Services.AddScoped<IGET_CreateBillAppRepository, SqlGET_CreateBillAppRepository>();

builder.Services.AddScoped<ISP_CheckAadhaardetailRepository, SqlSP_CheckAadhaardetailRepository>();

builder.Services.AddScoped<IGET_Report_FarmerbillRepository, SqlGET_Report_FarmerbillRepository>();

builder.Services.AddScoped<IGET_Report_VyapariLimitRepository, SqlGET_Report_VyapariLimitRepository>();

builder.Services.AddScoped<IGET_Report_VyapariBillRepository, SqlGET_Report_VyapariBillRepository>();

builder.Services.AddScoped<IGET_HIGHESTFRUITRATEEAuctionReposirory, SqlGET_HIGHESTFRUITRATEEAuctionReposirory>();

builder.Services.AddScoped<ISP_AddUpdVyapariSlabMasterRepository, SqlSP_AddUpdVyapariSlabMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariSlabDetailRepository, SqlSP_AddUpdVyapariSlabDetailRepository>();

builder.Services.AddScoped<IGET_VyapariSlabMasterRepository, SqlGET_VyapariSlabMasterRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariSlabMasterRepository, SqlSP_DeleteVyapariSlabMasterRepository>();

builder.Services.AddScoped<IGET_GateCashSummaryCashCountRepository, SqlGET_GateCashSummaryCashCountRepository>();

builder.Services.AddScoped<IGET_VyapariSlabMasterDataRepository, SqlGET_VyapariSlabMasterDataRepository>();

builder.Services.AddScoped<ISP_VerifyGateEntryRepository, SqlSP_VerifyGateEntryRepository>();

builder.Services.AddScoped<IGET_DashboardCountRepository, SqlGET_DashboardCountRepository>();

builder.Services.AddScoped<IGET_TopFarmerOnCratesRepository, SqlGET_TopFarmerOnCratesRepository>();

builder.Services.AddScoped<IGET_CreateFarmerBillVyapariRepository, SqlGET_CreateFarmerBillVyapariRepository>();

builder.Services.AddScoped<IGET_FarmerBillChargesVyapariRepository, SqlGET_FarmerBillChargesVyapariRepository>();

builder.Services.AddScoped<IGET_DayEndInfoRepository, SqlGET_DayEndInfoRepository>();

builder.Services.AddScoped<IGET_TopVyapariOnCratesRepository, SqlGET_TopVyapariOnCratesRepository>();

builder.Services.AddScoped<IGET_VyapariSchemeNameRepository, SqlGET_VyapariSchemeNameRepository>();

builder.Services.AddScoped<ISP_AddUpdCashCounterNameRepository, SqlSP_AddUpdCashCounterNameRepository>();

builder.Services.AddScoped<ISP_DeleteUserMastersRepository, SqlSP_DeleteUserMastersRepository>();

builder.Services.AddScoped<IGET_CashDetailsNearbyCashRepository, SqlGET_CashDetailsNearbyCashRepository>();

builder.Services.AddScoped<IGET_FarmerFamilyDetailRepository, SqlGET_FarmerFamilyDetailRepository>();

builder.Services.AddScoped<IGET_RokadVyapariSchemeNameRepository, SqlGET_RokadVyapariSchemeNameRepository>();

builder.Services.AddScoped<ISP_DayEndRepository, SqlSP_DayEndRepository>();

builder.Services.AddScoped<IGET_AuctionUserRepository, SqlGET_AuctionUserRepository>();

builder.Services.AddScoped<IGET_RokdaVyapariRepository, SqlGET_RokdaVyapariRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariJamaMasterRepository, SqlSP_AddUpdVyapariJamaMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariJamaDetailRepository, SqlSP_AddUpdVyapariJamaDetailRepository>();

builder.Services.AddScoped<IGET_VyaparijamaByVyapariDroDownRepository, SqlGET_VyaparijamaByVyapariDroDownRepository>();

builder.Services.AddScoped<IGET_VyapariJamaMasterRepository, SqlGET_VyapariJamaMasterRepository>();

builder.Services.AddScoped<IGET_TranspotersGateEntryDetailListRepository, SqlGET_TranspotersGateEntryDetailListRepository>();

builder.Services.AddScoped<IGET_GateEntry_VTRepository, SqlGET_GateEntry_VTRepository>();

builder.Services.AddScoped<ISP_UptFCMTokenFarmerRepository, SqlSP_UptFCMTokenFarmerRepository>();

builder.Services.AddScoped<ISP_UptFCMTokenTransporterRepository, SqlSP_UptFCMTokenTransporterRepository>();

builder.Services.AddScoped<IGET_PrecoolingSlabRepository, SqlGET_PrecoolingSlabRepository>();

builder.Services.AddScoped<IGET_VyapariDueBharanaDataRepository, SqlGET_VyapariDueBharanaDataRepository>();

builder.Services.AddScoped<IGET_DashBoardRateAuctionRepository, SqlGET_DashBoardRateAuctionRepository>();

builder.Services.AddScoped<IGET_SCHEMENAMERepository, SqlGET_SCHEMENAMERepository>();

builder.Services.AddScoped<IGET_CropWeekDataRepository, SqlGET_CropWeekDataRepository>();

builder.Services.AddScoped<IGET_CropMonthDataRepository, SqlGET_CropMonthDataRepository>();

builder.Services.AddScoped<IGET_VyapariSchemeDetailsRepository, SqlGET_VyapariSchemeDetailsRepository>();

builder.Services.AddScoped<IGET_ColdStorageOutWardRepository, SqlGET_ColdStorageOutWardRepository>();

builder.Services.AddScoped<ISP_DeleteColdStorageOutwardRepository, SqlSP_DeleteColdStorageOutwardRepository>();

builder.Services.AddScoped<IGET_Cashier_BalanceRepository, SqlGET_Cashier_BalanceRepository>();

builder.Services.AddScoped<ISP_AddVyapariVapasiRepository, SqlSP_AddVyapariVapasiRepository>();

builder.Services.AddScoped<IGET_TopfarmerOnPriceRepository, SqlGET_TopfarmerOnPriceRepository>();

builder.Services.AddScoped<IGET_ColdStorageOutWardByIdRepository, SqlGET_ColdStorageOutWardByIdRepository>();

builder.Services.AddScoped<IGET_OnlineAmountRepository, SqlGET_OnlineAmountRepository>();

builder.Services.AddScoped<ISP_CashCounterVoucherRepository, SqlSP_CashCounterVoucherRepository>();

builder.Services.AddScoped<IGET_VyapariVapasiDetailsRepository, SqlGET_VyapariVapasiDetailsRepository>();

builder.Services.AddScoped<ISP_UserLoginUpadateRepository, SqlSP_UserLoginUpadateRepository>();

builder.Services.AddScoped<ISP_AddUpdRokdaVyapariBharnaRepository, SqlSP_AddUpdRokdaVyapariBharnaRepository>();

builder.Services.AddScoped<IGET_VyapariDueRepository, SqlGET_VyapariDueRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariVapasiDetailsRepository, SqlSP_DeleteVyapariVapasiDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariJamaRepository, SqlSP_AddUpdVyapariJamaRepository>();


builder.Services.AddScoped<ISP_AddUpdRTGSReposiytory, SqlSP_AddUpdRTGSReposiytory>();

builder.Services.AddScoped<IGET_RegulerVaypariNameRepository, SqlGET_RegulerVaypariNameRepository>();

builder.Services.AddScoped<ISP_TransectionVoucherRepository, SqlSP_TransectionVoucherRepository>();

builder.Services.AddScoped<IGET_GetRTGSRepository, SqlGET_GetRTGSRepository>();

builder.Services.AddScoped<IGET_FarmerForNotificationsRepository, SqlGET_FarmerForNotificationsRepository>();

builder.Services.AddScoped<IGET_TransportersForNotificationsRepository, SqlGET_TransportersForNotificationsRepository>();

builder.Services.AddScoped<IGET_AdminNotificationForFarmerRepository, SqlGET_AdminNotificationForFarmerRepository>();

builder.Services.AddScoped<IGET_AdminNotificationForTransporterRepository, SqlGET_AdminNotificationForTransporterRepository>();

builder.Services.AddScoped<IGET_VyapariBharanaDataRepository, SqlGET_VyapariBharanaDataRepository>();

builder.Services.AddScoped<ISP_CheckCounterVoucherRepository, SqlSP_CheckCounterVoucherRepository>();

builder.Services.AddScoped<ISP_AddUpdSendSaveNotificationRepository, SqlSP_AddUpdSendSaveNotificationRepository>();

builder.Services.AddScoped<ISP_DeleteVyapariDueRepository, SqlSP_DeleteVyapariDueRepository>();

builder.Services.AddScoped<IGET_SaveNotificationRepository, SqlGET_SaveNotificationRepository>();

builder.Services.AddScoped<ISP_AddUpdVyapariAuctionViewRepository, SqlSP_AddUpdVyapariAuctionViewRepository>();

builder.Services.AddScoped<IMarkSheetRepository, MarkSheetRepository>();

builder.Services.AddScoped<IGET_VyapariAuctionViewRepository, SqlGET_VyapariAuctionViewRepository>();

builder.Services.AddScoped<IGET_FBillPrintRepository, SqlGET_FBillPrintRepository>();

builder.Services.AddScoped<IGET_RecentFarmerTokenRepository, SqlGET_RecentFarmerTokenRepository>();

builder.Services.AddScoped<ISP_AddUpdParkingRepository, SqlSP_AddUpdParkingRepository>();

builder.Services.AddScoped<IGET_PARKINGRepository, SqlGET_PARKINGRepository>();

builder.Services.AddScoped<ISP_DeleteParkingRepository, SQlSP_DeleteParkingRepository>();

builder.Services.AddScoped<IGET_GateEntryDetailByIdAppRepository, SqlGET_GateEntryDetailByIdAppRepository>();

builder.Services.AddScoped<IGET_GateDateRepository, SqlGET_GateDateRepository>();

builder.Services.AddScoped<IGET_Report_VyapariVakkalRepository, SqlGET_Report_VyapariVakkalRepository>();

builder.Services.AddScoped<IGET_RecentFarmerTokenAppRepository, SqlGET_RecentFarmerTokenAppRepository>();

builder.Services.AddScoped<IGET_VyapariBillChargesBillRepository, SqlGET_VyapariBillChargesBillRepository>();

builder.Services.AddScoped<ISP_DeleteGateEntryTokenAppRepository, SqlSP_DeleteGateEntryTokenAppRepository>();

builder.Services.AddScoped<ISP_AddUpdRokdaBharnaCompletedRepository, SqlSP_AddUpdRokdaBharnaCompletedRepository>();

builder.Services.AddScoped<ISP_DeleteAuctionMastersRepository, SqlSP_DeleteAuctionMastersRepository>();

builder.Services.AddScoped<IGET_CheckGateEntryVerifyRepository, SqlGET_CheckGateEntryVerifyRepository>();

builder.Services.AddScoped<IGET_PAIDReportRepository, SqlGET_PAIDReportRepository>();

builder.Services.AddScoped<IGET_CheckVehicalExistRepository, SqlGET_CheckVehicalExistRepository>();

builder.Services.AddScoped<ISP_AddUpdTransportersAppRepository, SqlSP_AddUpdTransportersAppRepository>();

builder.Services.AddScoped<IGetVyapriDetailsReportRepository, SqlGetVyapriDetailsReportRepository>();

builder.Services.AddScoped<IGET_VyapariPurchaseBillRRepositorycs, SqlGET_VyapariPurchaseBillRRepositorycs>();

builder.Services.AddScoped<IGET_Report_ServicesRepository, SqlGET_Report_ServicesRepository>();

builder.Services.AddScoped<IGET_Report_DayAuctionRepository, SqlGET_Report_DayAuctionRepository>();

builder.Services.AddScoped<ISP_UpdateVyapariDueRepository, SqlSP_UpdateVyapariDueRepository>();

builder.Services.AddScoped<IGET_REPORT_SRepository, SqlGET_REPORT_SRepository>();

builder.Services.AddScoped<IGET_CheckFarmarFaidRepository, SqlGET_CheckFarmarFaidRepository>();

builder.Services.AddScoped<IGET_CheckTranspoterTaidRepository, SqlGET_CheckTranspoterTaidRepository>();

builder.Services.AddScoped<ISP_UpdateRECEIPTMasterRepository, SqlSP_UpdateRECEIPTMasterRepository>();

builder.Services.AddScoped<ISP_UpdateVyapariBillMasterRepository, SqlSP_UpdateVyapariBillMasterRepository>();

builder.Services.AddScoped<ISP_UpdateAuctionshedMasterRepository, SqlSP_UpdateAuctionshedMasterRepository>();

builder.Services.AddScoped<IGET_NotifyTransporterOnTokenGenerationRepository, SqlGET_NotifyTransporterOnTokenGenerationRepository>();

builder.Services.AddScoped<IGET_NotifyFarmerOnTokenGenerationRepository, SqlGET_NotifyFarmerOnTokenGenerationRepository>();

builder.Services.AddScoped<ISP_AddUpdFarmerBankDetailRepository, SqlSP_AddUpdFarmerBankDetailRepository>();

builder.Services.AddScoped<IGET_RegulerVyapriBharanaRepository, SqlGET_RegulerVyapriBharanaRepository>();

builder.Services.AddScoped<IGET_FarmerTokenNotificationRepository, SqlGET_FarmerTokenNotificationRepository>();

builder.Services.AddScoped<IGET_VyapariTransectionReportRepository, SqlGET_VyapariTransectionReportRepository>();

builder.Services.AddScoped<IGET_CategoryMasterCaseRepository, SqlGET_CategoryMasterCaseRepository>();

builder.Services.AddScoped<ISP_VapasiVoucherRepository, SqlSP_VapasiVoucherRepository>();

builder.Services.AddScoped<IGET_TyreProductMasterRepository, SqlGET_TyreProductMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdDelarRateRepository, SqlSP_AddUpdDelarRateRepository>();

builder.Services.AddScoped<IGET_CustomerNameRepository, SqlGET_CustomerNameRepository>();

builder.Services.AddScoped<IGET_DelarRateRepository, SqlGET_DelarRateRepository>();

builder.Services.AddScoped<IGET_ProductDNameRepository, SqlGET_ProductDNameRepository>();

builder.Services.AddScoped<ISP_AddUpdVisitorMasterRepository, SqlSP_AddUpdVisitorMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdVisitorDetailRepository, SqlSP_AddUpdVisitorDetailRepository>();

builder.Services.AddScoped<ISP_DeleteDelarRateRepository, SqlSP_DeleteDelarRateRepository>();

builder.Services.AddScoped<IuploadPdfRepository, SqluploadPdfRepository>();

builder.Services.AddScoped<IGET_DealerSearchRepository, SqlGET_DealerSearchRepository>();

builder.Services.AddScoped<IGET_SchedulerMasterRepository, SqlGET_SchedulerMasterRepository>();

builder.Services.AddScoped<IGET_TyreCustomerPartnersRepository, SqlGET_TyreCustomerPartnersRepository>();

builder.Services.AddScoped<ISP_DeleteTyreCustomersPartnersRepository, SqlSP_DeleteTyreCustomersPartnersRepository>();

builder.Services.AddScoped<IGET_VisitorMasterRepository, SqlGET_VisitorMasterRepository>();

builder.Services.AddScoped<ISP_DeleteVisitorMasterRepository, SqlSP_DeleteVisitorMasterRepository>();

builder.Services.AddScoped<IGET_VisitorDataRepository, SqlGET_VisitorDataRepository>();

builder.Services.AddScoped<IGET_CreateFBillVyapariRepository, SQlGET_CreateFBillVyapariRepository>();

builder.Services.AddScoped<IGET_VBillChargesBillRepository, SqlGET_VBillChargesBillRepository>();

builder.Services.AddScoped<IGET_ShedulerMasterDataRepository, SqlGET_ShedulerMasterDataRepository>();

builder.Services.AddScoped<IGET_PackingMaterialRepository, SqlGET_PackingMaterialRepository>();

builder.Services.AddScoped<ISP_AddUpdPackingMaterialRepository, SqlSP_AddUpdPackingMaterialRepository>();

builder.Services.AddScoped<ISP_DeletePackingMaterialRepository, SqlSP_DeletePackingMaterialRepository>();

builder.Services.AddScoped<IGET_CategoryDropdownInvRepository, SqlGET_CategoryDropdownInvRepository>();

builder.Services.AddScoped<ISP_AddUpdShedulerDetailRepository, SqlSP_AddUpdShedulerDetailRepository>();

builder.Services.AddScoped<ISP_AddUpdShedulerMasterRepository, SqlSP_AddUpdShedulerMasterRepository>();

builder.Services.AddScoped<IGET_PackingMaterialDropdownRepository, SqlGET_PackingMaterialDropdownRepository>();

builder.Services.AddScoped<IGET_CropInvertDataRepository, SqlGET_CropInvertDataRepository>();

builder.Services.AddScoped<IGET_FARMERFROMCONTACTRepository, SqlGET_FARMERFROMCONTACTRepository>();

builder.Services.AddScoped<IGET_ShedulerDetailDataRepository, SqlGET_ShedulerDetailDataRepository>();

builder.Services.AddScoped<ISP_updateVoucherRepository, SqlSP_updateVoucherRepository>();

builder.Services.AddScoped<ISP_AddUpdCropInvertDetailRepository, SqlSP_AddUpdCropInvertDetailRepository>();

builder.Services.AddScoped<ISP_AddUpdCropInvertMasterRepository, SqlSP_AddUpdCropInvertMasterRepository>();

builder.Services.AddScoped<IGET_CropInvertData1Repository, SqlGET_CropInvertData1Repository>();

builder.Services.AddScoped<IGET_CropInvertDetailsByIdRepository, SqlGET_CropInvertDetailsByIdRepository>();

builder.Services.AddScoped<IGET_CropInvertMasterRepository, SqlGET_CropInvertMasterRepository>();

builder.Services.AddScoped<IGET_DayBookDataRepository, SqlGET_DayBookDataRepository>();

builder.Services.AddScoped<ISP_AddUpdStockDetailsRepository, SqlSP_AddUpdStockDetailsRepository>();

builder.Services.AddScoped<IReceiptRepository, ReceiptRepository>();

builder.Services.AddScoped<IGET_CropInvertBillRepository, SqlGET_CropInvertBillRepository>();

builder.Services.AddScoped<IGET_CropInvertDateCheckRepository, SqlGET_CropInvertDateCheckRepository>();

builder.Services.AddScoped<ISP_TrialBalanceDataRepository, SqlSP_TrialBalanceDataRepository>();

builder.Services.AddScoped<IGET_TrialBalanceDataRepository, SqlGET_TrialBalanceDataRepository>();

builder.Services.AddScoped<IGET_QuatationSRNoRepository, SqlGET_QuatationSRNoRepository>();

builder.Services.AddScoped<IGET_ProfitLossDataRepository, SqlGET_ProfitLossDataRepository>();

builder.Services.AddScoped<ISP_DeletePGoodReceiptNoteRepository, SqlSP_DeletePGoodReceiptNoteRepository>();

builder.Services.AddScoped<ISP_AddUpdCropBillRepository, SqlSP_AddUpdCropBillRepository>();

builder.Services.AddScoped<ISP_AddUpdCropBillDetailRepository, SqlSP_AddUpdCropBillDetailRepository>();

builder.Services.AddScoped<ISP_DeletePReturnMasterRepository, SqlSP_DeletePReturnMasterRepository>();

builder.Services.AddScoped<IGET_CropBillDetailRepository, SqlGET_CropBillDetailRepository>();

builder.Services.AddScoped<IGET_CropBillRepository, SqlGET_CropBillRepository>();

builder.Services.AddScoped<IGET_BalanceshitDataRepository, SqlGET_BalanceshitDataRepository>();

builder.Services.AddScoped<IGET_GenralAccountRepository, SqlGET_GenralAccountRepository>();

builder.Services.AddScoped<IGET_LedgerDataRepository, SqlGET_LedgerDataRepository>();

builder.Services.AddScoped<IGET_TraderNameDropdownRepository, SqlGET_TraderNameDropdownRepository>();

builder.Services.AddScoped<IGET_PQuatationNoCheckRepository, SqlGET_PQuatationNoCheckRepository>();

builder.Services.AddScoped<IGET_POrderNoCheckRepository, SqlGET_POrderNoCheckRepository>();

builder.Services.AddScoped<IGET_PRequisitionNoCheckRepository, SqlGET_PRequisitionNoCheckRepository>();

builder.Services.AddScoped<ISP_AddUpdtStockLoadingVehicleMasterRepository, SqlSP_AddUpdtStockLoadingVehicleMasterRepository>();

builder.Services.AddScoped<IGET_CropVehicleLoadingDataRepository, SqlGET_CropVehicleLoadingDataRepository>();

builder.Services.AddScoped<IGET_PChallanNoCheckRepository, SqlGET_PChallanNoCheckRepository>();

builder.Services.AddScoped<ISP_AddUpdStockLoadingVehicleDetailRepository, SqlSP_AddUpdStockLoadingVehicleDetailRepository>();

builder.Services.AddScoped<IGET_StockLoadingVehicleMasterRepository, SqlGET_StockLoadingVehicleMasterRepository>();

builder.Services.AddScoped<ISP_DeleteStockLoadingVehicleMasterRepository, SqlSP_DeleteStockLoadingVehicleMasterRepository>();

builder.Services.AddScoped<IGET_SQuatationForProceedRepository, SqlGET_SQuatationForProceedRepository>();

builder.Services.AddScoped<IGET_MasterDetailInvertDataRepository, SqlGET_MasterDetailInvertDataRepository>();

builder.Services.AddScoped<IGET_chalanNORepository, SqlGET_chalanNORepository>();

builder.Services.AddScoped<ISP_PBillVoucherRepository, SqlSP_PBillVoucherRepository>();

builder.Services.AddScoped<IGET_BillNORepository, SqlGET_BillNORepository>();

builder.Services.AddScoped<ISP_SBillVoucherRepository, SqlSP_SBillVoucherRepository>();

builder.Services.AddScoped<IGET_StockLoadingVehicleDetailRepository, SqlGET_StockLoadingVehicleDetailRepository>();

builder.Services.AddScoped<IGET_CropTransportInvoiceDataRepository, SqlGET_CropTransportInvoiceDataRepository>();

builder.Services.AddScoped<ISP_AddUpdCropTransportInvoiceRepository, SqlSP_AddUpdCropTransportInvoiceRepository>();

builder.Services.AddScoped<ISP_AddUpdCropTransportInvoiceDetailRepository, SqlSP_AddUpdCropTransportInvoiceDetailRepository>();

builder.Services.AddScoped<IGET_VisitorMasterApprovalRepository, SqlGET_VisitorMasterApprovalRepository>();

builder.Services.AddScoped<IGET_ScheduleDetailsRepository, SqlGET_ScheduleDetailsRepository>();

builder.Services.AddScoped<ISP_AddUpdSabhasadMasterRepository, SqlSP_AddUpdSabhasadMasterRepository>();

builder.Services.AddScoped<ISP_AddUpdSFamilyInformationRepository, SqlSP_AddUpdSFamilyInformationRepository>();

builder.Services.AddScoped<ISP_AddUpdSAgriculturalInformationRepository, SqlSP_AddUpdSAgriculturalInformationRepository>();

builder.Services.AddScoped<ISP_AddUpdSCropInformationRepository, SqlSP_AddUpdSCropInformationRepository>();

builder.Services.AddScoped<ISP_AddUpdSChemicalNameRepository, SqlSP_AddUpdSChemicalNameRepository>();

builder.Services.AddScoped<ISP_AddUpdSFertilizersNameRepository, SqlSP_AddUpdSFertilizersNameRepository>();

builder.Services.AddScoped<IGET_SabhasadMasterRepository, SqlGET_SabhasadMasterRepository>();

builder.Services.AddScoped<IGET_SFamilyInformationRepository, SqlGET_SFamilyInformationRepository>();

builder.Services.AddScoped<ISP_AddUpdShedulerDetail_InvardRepository, SqlSP_AddUpdShedulerDetail_InvardRepository>();

builder.Services.AddScoped<ISP_DeleteSabhasadMasterRepository, SqlSP_DeleteSabhasadMasterRepository>();

builder.Services.AddScoped<ISP_DeleteSFamilyInformationRepository, SqlSP_DeleteSFamilyInformationRepository>();

builder.Services.AddScoped<IGET_SAgriculturalInformationRepository, SqlGET_SAgriculturalInformationRepository>();

builder.Services.AddScoped<IGET_SCropInformationRepository, SqlGET_SCropInformationRepository>();

builder.Services.AddScoped<IGET_SFertilizersNameRepository, SqlGET_SFertilizersNameRepository>();

builder.Services.AddScoped<IGET_SChemicalNameRepository, SqlGET_SChemicalNameRepository>();

builder.Services.AddScoped<IGET_SContactNumberCheckRepository, SqlGET_SContactNumberCheckRepository>();

builder.Services.AddScoped<ISP_AddAvdTranRepository, SqlSP_AddAvdTranRepository>();

builder.Services.AddScoped<ISP_UpdVisitorAdvanceRepository, SqlSP_UpdVisitorAdvanceRepository>();

builder.Services.AddScoped<IGET_SabhasadContactNumberCheckRepository, SqlGET_SabhasadContactNumberCheckRepository>();

builder.Services.AddScoped<ISP_AddUpdAllTransectionsRepository, SqlSP_AddUpdAllTransectionsRepository>();

builder.Services.AddScoped<IGET_AllTransectionsDataRepository, SqlGET_AllTransectionsDataRepository>();

builder.Services.AddScoped<IGET_CropBillViewRepository, SqlGET_CropBillViewRepository>();

builder.Services.AddScoped<IGET_CropTransportInvoiceRepository, SqlGET_CropTransportInvoiceRepository>();

builder.Services.AddScoped<IGET_TotalPurchaseReportRepository, SqlGET_TotalPurchaseReportRepository>();

builder.Services.AddScoped<ISP_DeleteVisitorDetailRepository, SqlSP_DeleteVisitorDetailRepository>();

builder.Services.AddScoped<ISP_DeleteCropInvertDetailRepository, SqlSP_DeleteCropInvertDetailRepository>();

builder.Services.AddScoped<IGET_Report_SalesRepository, SqlGET_Report_SalesRepository>();

builder.Services.AddScoped<IGET_CropBillDataRepository, SqlGET_CropBillDataRepository>();

builder.Services.AddScoped<IGET_FarmerBillDataRepository, SqlGET_FarmerBillDataRepository>();

builder.Services.AddScoped<IGET_SingleRowCropInvertDetailRepository, SqlGET_SingleRowCropInvertDetailRepository>();

builder.Services.AddScoped<IGET_FarmerTranDataRepository, SqlGET_FarmerTranDataRepository>();

builder.Services.AddScoped<IGET_FarmerCropBillDataRepository, SqlGET_FarmerCropBillDataRepository>();

builder.Services.AddScoped<IGET_FarmerLedgerReportRepository, SqlGET_FarmerLedgerReportRepository>();

builder.Services.AddScoped<IGET_FarmerPaymentSummaryRepository, SqlGET_FarmerPaymentSummaryRepository>();

builder.Services.AddScoped<ISP_UpdFarmerBillStatusRepository, SqlSP_UpdFarmerBillStatusRepository>();

builder.Services.AddScoped<IGET_FarmerAdvanceRepository, SqlGET_FarmerAdvanceRepository>();

builder.Services.AddScoped<ISP_UpdFarmerStatusRepository, SqlSP_UpdFarmerStatusRepository>();

builder.Services.AddScoped<ISP_DeleteFarmerTranRepository, SqlSP_DeleteFarmerTranRepository>();

builder.Services.AddScoped<IGET_TransportBillDataRepository, SqlGET_TransportBillDataRepository>();

builder.Services.AddScoped<ISP_DeleteSchedulerDetailRepository, SqlSP_DeleteSchedulerDetailRepository>();

builder.Services.AddScoped<IGET_CustomerTransportBillDataRepository, SqlGET_CustomerTransportBillDataRepository>();

builder.Services.AddScoped<IGET_CustomerTranDataRepository, SqlGET_CustomerTranDataRepository>();

builder.Services.AddScoped<IGET_AllTranDataCustomerRepository, SqlGET_AllTranDataCustomerRepository>();

builder.Services.AddScoped<ISP_UpdFCropBillStatusRepository, SqlSP_UpdFCropBillStatusRepository>();

builder.Services.AddScoped<ISP_UpdateTransportBillRepository, SqlSP_UpdateTransportBillRepository>();

builder.Services.AddScoped<IGET_ACCSESSRIGHTDropdownSFCRepository, SqlGET_ACCSESSRIGHTDropdownSFCRepository>();

builder.Services.AddScoped<ISP_DeleteShedulerMasterRepository, SqlSP_DeleteShedulerMasterRepository>();

builder.Services.AddScoped<ISP_DeleteCropInvertRepository, SqlSP_DeleteCropInvertRepository>();

builder.Services.AddScoped<IGET_VechicleProfitReportRepository, SqlGET_VechicleProfitReportRepository>();

builder.Services.AddScoped<IGET_CropDirectInvertMasterRepository, SQlGET_CropDirectInvertMasterRepository>();

builder.Services.AddScoped<IGET_CropDirectInvertDetailRepository, SqlGET_CropDirectInvertDetailRepository>();

builder.Services.AddScoped<IDirectInvertRepository, SqlDirectInvertRepository>();

builder.Services.AddScoped<IGET_SChallanProceedRepository, SqlGET_SChallanProceedRepository>();

builder.Services.AddScoped<ISP_DeleteCropDirectInvertDetailRepository, SqlSP_DeleteCropDirectInvertDetailRepository>();

builder.Services.AddScoped<ISP_InsertMonthlyClosingBalRepository, SqlSP_InsertMonthlyClosingBalRepository>();

builder.Services.AddScoped<ISP_CheckMobileNumberRepository, SqlSP_CheckMobileNumberRepository>();

builder.Services.AddScoped<IGET_SBillNoRepository, SqlGET_SBillNoRepository>();

builder.Services.AddScoped<IGET_SReturnBillNoRepository, SqlGET_SReturnBillNoRepository>();

builder.Services.AddScoped<IGET_SuggestPhoneNumberRepository, SqlGET_SuggestPhoneNumberRepository>();

builder.Services.AddScoped<IGET_PReturnBillNoRepository, SqlGET_PReturnBillNoRepository>();

builder.Services.AddScoped<IGET_CheckSRNO_ExistRepository, SqlGET_CheckSRNO_ExistRepository>();

builder.Services.AddScoped<IGET_challan_from_Return_masterRepository, SqlGET_challan_from_Return_masterRepository>();

builder.Services.AddScoped<IGET_CustomerLedgerRepository, SqlGET_CustomerLedgerRepository>();

builder.Services.AddScoped<IGET_SalesReportRepository, SqlGET_SalesReportRepository>();







//// Services
//builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

//var jwtConfig = builder.Configuration.GetSection("Jwt");

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtConfig["Issuer"],
//            ValidAudience = jwtConfig["Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(jwtConfig["Key"]!)
//            )
//        };
//    });

builder.Services.AddAuthorization();


builder.Services.AddHttpClient();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

string filePath = @"C:\Users\LENOVO\source\repos\Project\Project\wwwroot\firebase-service-account.json";

if (File.Exists(filePath))
{
    Console.WriteLine("File exists.");
}
else
{
    Console.WriteLine("File does not exist.");
}

var app = builder.Build();

// Use environment-specific CORS policy
var corsPolicy = app.Environment.IsDevelopment() ? "AllowAll" : "Production";
app.UseCors(corsPolicy); // ✅ Ensure CORS is applied before anything else

// Set path base for production IIS sub-path deployment
// Use lowercase to match frontend API calls (/backend not /Backend)
if (app.Environment.IsProduction())
{
    app.UsePathBase("/backend");
}

// Configure the HTTP request pipeline - Keep Swagger enabled for all environments
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

//app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthentication();   // ✅ FIRST
app.UseAuthorization();    // ✅ AFTER authentication

app.MapControllers();

app.Run();
