using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsAppCrossMobile.Responses
{
    public abstract class DocumentBase<T> : ObservableObject
    {
        private static TimeSpan MinimumTime = TimeSpan.FromMilliseconds(500);

        private DateTime createAt;
        public DateTime CreateAt
        {
            get { return createAt; }
            set
            {
                createAt = value;
                base.RaisePropertyChanged();
            }
        }

        public string TimePassed
        {
            get
            {
                var ts = DateTime.UtcNow.Subtract(this.CreateAt);

                if (ts > MinimumTime)
                {
                    if (ts.Minutes > 0)
                    {
                        return $"{(int)ts.TotalMinutes}min {(int)ts.Seconds}sec fa";
                    }
                    else
                    {
                        return $"{(int)ts.Seconds}sec fa";
                    }
                }
                else
                {
                    return "adesso";
                }
            }
        }

        protected DocumentBase()
        {
            this.CreateAt = DateTime.UtcNow;
        }

        public T Id { get; set; }
    }
}