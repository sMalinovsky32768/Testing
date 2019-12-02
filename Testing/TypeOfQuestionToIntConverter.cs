using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Testing
{
    class TypeOfQuestionToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((TypeOfQuestion)value)
            {
                case TypeOfQuestion.oneCorrect:
                    return 0;
                case TypeOfQuestion.manyCorrect:
                    return 1;
                case TypeOfQuestion.inputAnswer:
                    return 2;
                case TypeOfQuestion.accordance:
                    return 3;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((int)value)
            {
                case 0:
                    return TypeOfQuestion.oneCorrect;
                case 1:
                    return TypeOfQuestion.manyCorrect;
                case 2:
                    return TypeOfQuestion.inputAnswer;
                case 3:
                    return TypeOfQuestion.accordance;
            }
            return null;
        }
    }
}
