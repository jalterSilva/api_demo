using FluentValidation;
using GSP.API.Core.Models.Base;
using Newtonsoft.Json;

namespace GSP.API.Core.Models.Broker
{
    public class BrokerModel : BaseModel
    {
        #region Properties
        public static int Max_Length_Broker_Name = 50;
        public static int Max_Length_CNPJ = 14;
        public static int Min_Length_CNPJ = 14;

        public string CNPJ { get; private set; }
        public string BrokerName { get; private set; }
        public bool Active { get; private set; }

        #endregion

        #region Constructor

        public BrokerModel()
        {
        }
        public BrokerModel(string cnpj, string brokerName)
        {
            CNPJ = cnpj;
            BrokerName = brokerName;
            SetActive();
            ValidateModel();
        }

        [JsonConstructor]
        public BrokerModel(int id, string cnpj, string brokerName, bool active)
        {
            SetId(id);
            CNPJ = cnpj;
            BrokerName = brokerName;
            Active = active;       
            ValidateModel();
        }
        #endregion

        #region Methods

        public void SetCNPJ(string cnpj)
        {
            CNPJ = cnpj;
            ValidateModel();

        }
        public void SetBrokerName(string brokerName)
        {
            BrokerName = brokerName;
            ValidateModel();
        }

        public void SetActive()
        {
            Active = true;

        }

        public void SetInactive()
        {
            Active = false;

        }

        public void SetStatus(bool status)
        {
            Active = status;

        }

        #endregion

        #region Validate Model
        public override void ValidateModel()
        {
            ValidationResult = new BrokerModelValidation().Validate(this);
        }
        #endregion

        #region Fluent Validation
        public class BrokerModelValidation : AbstractValidator<BrokerModel>
        {
            public BrokerModelValidation()
            {

                RuleFor(x => x.BrokerName).NotEmpty().When(x => string.IsNullOrEmpty(x.BrokerName)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.BrokerName).MaximumLength(Max_Length_Broker_Name).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_Broker_Name}");

                RuleFor(x => x.CNPJ).NotEmpty().When(x => string.IsNullOrEmpty(x.CNPJ)).WithMessage("{PropertyName} cannot be empty");
                RuleFor(x => x.CNPJ).MaximumLength(Max_Length_CNPJ).WithMessage($"{{PropertyName}} lenght cannot by greater than {Max_Length_CNPJ}");
                RuleFor(x => x.CNPJ).MinimumLength(Min_Length_CNPJ).WithMessage($"{{PropertyName}} lenght cannot by less than {Min_Length_CNPJ}");

            }
        }
        #endregion

    }
}
