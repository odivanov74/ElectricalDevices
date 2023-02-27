using ElectricalDevicesCW.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElectricalDevicesCW.Services
{
    public class SearchService
    {
        DataBaseService dataBaseService = new DataBaseService();
        public string CmdSearch { get; private set; }
        public string CmdPartFraction { get; private set; }
        public string CmdAllFraction { get; private set; }
        public string CmdQuantity { get; private set; }

        public async Task<string> Search(string valueSelect1, string paramString1,
                                         string valueSelect2, string paramString2,
                                         string valueSelect3, string paramString3,
                                         string partAreaSearch, string allAreaSearch,
                                         bool Part1Check, bool Part2Check, bool Part3Check,
                                         bool All1Check, bool All2Check, bool All3Check,
                                         string inValue, string inStartValue, string inEndValue,
                                         DateTimePicker inDate, DateTimePicker inStartDate, DateTimePicker inEndDate,
                                         bool isDefected)
        {
            #region подоготовительные действия            
            int resultValue = 0;
            int startValue = 0;
            int endValue = 0;
            string date = "";
            string startDate = "";
            string endDate = "";
            string defected = "";

            string str = "";
            string innerMainStr = "";

            string innerPartFractionStr = "";
            string innerAllFractionStr = "";
            string innerQuantityStr = "";

            string part1Str = "";
            string part2Str = "";
            string wherePartStr = "";

            string part1FractionStr = "";
            string part2FractionStr = "";
            string all1FractionStr = "";
            string all2FractionStr = "";

            string wherePartFractionStr = "";
            string whereAllFractionStr = "";

            string partAreaStr = "";
            string allAreaStr = "";
            string quantityPart = "";
            string quantityAll = "";

            if (partAreaSearch == "в продано")
            {
                partAreaStr = "order_FK is not null";
                quantityPart = "saled";
            }
            else if (partAreaSearch == "на складе")
            {
                partAreaStr = "order_FK is null";
                quantityPart = "stock_balance";
            }
            else if (partAreaSearch == "в резервах")
            {
                partAreaStr = "basket_FK is not null";
                quantityPart = "reserved";
            }
            else
            {
                quantityPart = "stock_balance + saled";
            }

            if (allAreaSearch == "проданного")
            {
                allAreaStr = "order_FK is not null";
                quantityAll = "stock_balance + saled";
            }
            else if (allAreaSearch == "склада")
            {
                allAreaStr = "order_FK is null";
                quantityAll = "stock_balance";
            }
            else if (allAreaSearch == "резервов")
            {
                allAreaStr = "basket_FK is not null";
                quantityAll = "reserved";
            }
            else
            {
                quantityAll = "stock_balance + saled";
            }

            innerMainStr = GetInnerStr(partAreaSearch, allAreaSearch, valueSelect1, paramString1, valueSelect2, paramString2, valueSelect3, paramString3);
            innerPartFractionStr = GetInnerStr(partAreaSearch, allAreaSearch,
                                                valueSelect1, paramString1,
                                                valueSelect2, paramString2,
                                                valueSelect3, paramString3,
                                                Part1Check, Part2Check, Part3Check);
            innerAllFractionStr = GetInnerStr(partAreaSearch, allAreaSearch, valueSelect1, paramString1, valueSelect2, paramString2, valueSelect3, paramString3,
                                                All1Check, All2Check, All3Check);
            if (innerAllFractionStr.Contains("device") == false) innerAllFractionStr += "inner join device on model_FK = model.model_id ";
            if (innerPartFractionStr.Contains("device") == false) innerPartFractionStr += "inner join device on model_FK = model.model_id ";
            if (innerMainStr.Contains("device") == false) innerQuantityStr = innerMainStr + "inner join device on model_FK = model.model_id ";
            else innerQuantityStr = innerMainStr;

            switch (valueSelect1)
            {
                case "type":
                case "manufacturer":
                case "supplier":
                case "country":
                    if (paramString2 != "-")
                    {
                        if (paramString3 != "-")
                        {
                            if (valueSelect1 == valueSelect2)
                            {
                                if (valueSelect2 == valueSelect3)
                                {
                                    part1Str = $"({valueSelect1}_name = '{paramString1}' or {valueSelect2}_name = '{paramString2}' or {valueSelect3}_name = '{paramString3}') ";
                                    part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "or",
                                                                           Part2Check, valueSelect2, paramString2, "or",
                                                                           Part3Check, valueSelect3, paramString3);
                                    all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "or",
                                                                          All2Check, valueSelect2, paramString2, "or",
                                                                          All3Check, valueSelect3, paramString3);
                                }
                                else
                                {
                                    part1Str = $"({valueSelect1}_name = '{paramString1}' or {valueSelect2}_name = '{paramString2}') and {valueSelect3}_name = '{paramString3}' ";
                                    part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "or",
                                                                           Part2Check, valueSelect2, paramString2, "and",
                                                                           Part3Check, valueSelect3, paramString3);
                                    all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "or",
                                                                           All2Check, valueSelect2, paramString2, "and",
                                                                           All3Check, valueSelect3, paramString3);
                                }
                            }
                            else if (valueSelect1 == valueSelect3)
                            {
                                part1Str = $"({valueSelect1}_name = '{paramString1}' or {valueSelect3}_name = '{paramString3}') and {valueSelect2}_name = '{paramString2}' ";
                                part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "or",
                                                                       Part3Check, valueSelect3, paramString3, "and",
                                                                       Part2Check, valueSelect2, paramString2);
                                all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "or",
                                                                      All3Check, valueSelect3, paramString3, "and",
                                                                      All2Check, valueSelect2, paramString2);
                            }
                            else if (valueSelect2 == valueSelect3)
                            {
                                part1Str = $"({valueSelect2}_name = '{paramString2}' or {valueSelect3}_name = '{paramString3}') and {valueSelect1}_name = '{paramString1}' ";
                                part1FractionStr = GetFractionWhereStr(Part2Check, valueSelect2, paramString2, "or",
                                                                       Part3Check, valueSelect3, paramString3, "and",
                                                                       Part1Check, valueSelect1, paramString1);
                                all1FractionStr = GetFractionWhereStr(All2Check, valueSelect2, paramString2, "or",
                                                                      All3Check, valueSelect3, paramString3, "and",
                                                                      All1Check, valueSelect1, paramString1);
                            }
                            else
                            {
                                part1Str = $"{valueSelect2}_name = '{paramString2}' and {valueSelect3}_name = '{paramString3}' and {valueSelect1}_name = '{paramString1}' ";
                                part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "and",
                                                                       Part2Check, valueSelect2, paramString2, "and",
                                                                       Part3Check, valueSelect3, paramString3);
                                all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "and",
                                                                      All2Check, valueSelect2, paramString2, "and",
                                                                      All3Check, valueSelect3, paramString3);
                            }
                        }
                        else
                        {
                            if (valueSelect1 == valueSelect2)
                            {
                                part1Str = $"({valueSelect1}_name = '{paramString1}' or {valueSelect2}_name = '{paramString2}') ";
                                part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "or", Part2Check, valueSelect2, paramString2);
                                all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "or", All2Check, valueSelect2, paramString2);
                            }
                            else
                            {
                                part1Str = $"{valueSelect1}_name = '{paramString1}' and {valueSelect2}_name = '{paramString2}' ";
                                part1FractionStr = GetFractionWhereStr(Part1Check, valueSelect1, paramString1, "and", Part2Check, valueSelect2, paramString2);
                                all1FractionStr = GetFractionWhereStr(All1Check, valueSelect1, paramString1, "and", All2Check, valueSelect2, paramString2);
                            }
                        }
                    }
                    else
                    {
                        if(paramString1 != "-") part1Str = $"{valueSelect1}_name = '{paramString1}' ";
                        if (Part1Check) part1FractionStr = part1Str;
                        if (All1Check) all1FractionStr = part1Str;
                    }

                    break;
                case "price":
                case "weight":
                case "quantity":
                case "manufacture_date":
                case "order_date":

                     if (paramString1 == "Значение" || paramString1 == "Больше" || paramString1 == "Меньше")
                    {
                        if (valueSelect1 == "price" || valueSelect1 == "weight" || valueSelect1 == "quantity") if (int.TryParse(inValue, out resultValue) == false) return "некорректные данные!";

                        if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                            date = inDate.Value.Year.ToString() + "-" + inDate.Value.Month.ToString() + "-" + inDate.Value.Day.ToString();
                    }

                    if (paramString1 == "Диапазон")
                    {
                        if (valueSelect1 == "price" || valueSelect1 == "weight" || valueSelect1 == "quantity")
                            if (int.TryParse(inStartValue, out startValue) == false || int.TryParse(inEndValue, out endValue) == false) return "некорректные данные!";

                        if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                        {
                            startDate = inStartDate.Value.Year.ToString() + "-" + inStartDate.Value.Month.ToString() + "-" + inStartDate.Value.Day.ToString();
                            endDate = inEndDate.Value.Year.ToString() + "-" + inEndDate.Value.Month.ToString() + "-" + inEndDate.Value.Day.ToString();
                        }
                    }

                    if (paramString2 != "-")
                    {
                        if (paramString3 != "-")
                        {
                            if (valueSelect2 == valueSelect3)
                            {
                                part2Str = $"({valueSelect2}_name = '{paramString2}' or {valueSelect3}_name = '{paramString3}')";
                                part2FractionStr = GetFractionWhereStr(Part2Check, valueSelect2, paramString2, "or",
                                                                       Part3Check, valueSelect3, paramString3);

                                all2FractionStr = GetFractionWhereStr(All2Check, valueSelect2, paramString2, "or",
                                                                      All3Check, valueSelect3, paramString3);
                            }
                            else
                            {
                                part2Str = $"{valueSelect2}_name = '{paramString2}' and {valueSelect3}_name = '{paramString3}'";
                                part2FractionStr = GetFractionWhereStr(Part2Check, valueSelect2, paramString2, "and",
                                                                       Part3Check, valueSelect3, paramString3);
                                all2FractionStr = GetFractionWhereStr(All2Check, valueSelect2, paramString2, "and",
                                                                      All3Check, valueSelect3, paramString3);
                            }
                        }
                        else
                        {
                            part2Str = $"{valueSelect2}_name = '{paramString2}'";

                            if (Part2Check)
                            {
                                part2FractionStr = part2Str;
                            }
                            if (All2Check)
                            {
                                all2FractionStr = part2Str;
                            }
                        }
                    }



                    switch (paramString1)
                    {
                        case "-":

                            CmdSearch = $"Select distinct model.model_id, model_name, type_FK, weight, price, stock_balance, reserved, saled, manufacturer_FK, supplier_FK from model " +
                                          $"{innerMainStr} {GetWhereStr("", "", partAreaStr)};";
                            CmdPartFraction = $"select count(device_id) as PartFraction from model {innerPartFractionStr} {GetWhereStr("", "", partAreaStr)};";
                            CmdAllFraction =  $"select count(device_id) as AllFraction from model {innerAllFractionStr} { GetWhereStr("", "", allAreaStr)};";
                            CmdQuantity =     $"select count(device_id) as Quantity from model {innerQuantityStr} {GetWhereStr("", "", partAreaStr)};";
                            str = await dataBaseService.SearchModelTableAsync(CmdSearch, CmdPartFraction, CmdAllFraction, CmdQuantity);
                            return str;                            
                        case "Значение":
                            if (valueSelect1 == "price" || valueSelect1 == "weight")
                            {
                                part1Str = $"{valueSelect1} = {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} = {resultValue} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} = {resultValue} ";
                            }
                            if (valueSelect1 == "quantity")
                            {
                                part1Str = $"{quantityPart} = {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} = {resultValue} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} = {resultValue} ";
                            }
                            if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                            {
                                part1Str = $"{valueSelect1} = '{date}' ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} = '{date}' ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} = '{date}' ";
                            }
                            break;
                        case "Максимум":
                            if (valueSelect1 != "quantity")
                            {
                                part1Str = $"{valueSelect1} = (select max({valueSelect1}) from model {innerMainStr} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} = (select max({valueSelect1}) from model {innerMainStr} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} = (select max({valueSelect1}) from model {innerMainStr} ";
                            }
                            else
                            {
                                part1Str = $"{quantityPart} = (select max({quantityPart}) from model {innerMainStr} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} = (select max({quantityPart}) from model {innerMainStr} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} = (select max({quantityAll}) from model {innerMainStr} ";
                            }
                               
                            
                            break;
                        case "Минимум":
                            if (valueSelect1 != "quantity")
                            {
                                part1Str = $"{valueSelect1} = (select min({valueSelect1}) from model {innerMainStr} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} = (select min({valueSelect1}) from model {innerMainStr} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} = (select min({valueSelect1}) from model {innerMainStr} ";
                            }
                            else
                            {
                                part1Str = $"{quantityPart} = (select min({quantityPart}) from model {innerMainStr} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} = (select min({quantityPart}) from model {innerMainStr} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} = (select min({quantityAll}) from model {innerMainStr} ";
                            }
                            
                            break;
                        case "Среднее":                            
                            break;
                        case "Больше":
                            if (valueSelect1 == "price" || valueSelect1 == "weight")
                            {
                                part1Str = $"{valueSelect1} > {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} > {resultValue} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} > {resultValue} ";
                            }
                            if (valueSelect1 == "quantity")
                            {
                                part1Str = $"{quantityPart} > {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} > {resultValue} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} > {resultValue} ";
                            }
                            if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                            {
                                part1Str = $"{valueSelect1} > '{date}' ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} > '{date}' ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} > '{date}' ";
                            }
                            break;
                        case "Меньше":
                            if (valueSelect1 == "price" || valueSelect1 == "weight")
                            {
                                part1Str = $"{valueSelect1} < {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} < {resultValue} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} < {resultValue} ";
                            }
                            if (valueSelect1 == "quantity")
                            {
                                part1Str = $"{quantityPart} < {resultValue} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} < {resultValue} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} < {resultValue} ";
                            }
                            if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                            {
                                part1Str = $"{valueSelect1} < '{date}' ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} < '{date}' ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} < '{date}' ";
                            }
                            break;
                        case "Больше среднего":
                            part1Str = $"{valueSelect1} > (select avg({valueSelect1}) from model ";
                            if (Part1Check) part1FractionStr = $"{valueSelect1} > (select avg({valueSelect1}) from model {innerMainStr} ";
                            if (All1Check) all1FractionStr = $"{valueSelect1} > (select avg({valueSelect1}) from model {innerMainStr} ";
                            break;
                        case "Меньше среднего":
                            part1Str = $"{valueSelect1} < (select avg({valueSelect1}) from model ";
                            if (Part1Check) part1FractionStr = $"{valueSelect1} < (select avg({valueSelect1}) from model {innerMainStr} ";
                            if (All1Check) all1FractionStr = $"{valueSelect1} < (select avg({valueSelect1}) from model {innerMainStr} ";
                            break;
                        case "Диапазон":
                            if (valueSelect1 == "price" || valueSelect1 == "weight")
                            {
                                part1Str = $"{valueSelect1} >= { startValue} and {valueSelect1} <= {endValue} ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} >= { startValue} and {valueSelect1} <= {endValue} ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} >= { startValue} and {valueSelect1} <= {endValue} ";
                            }
                            if (valueSelect1 == "quantity")
                            {
                                part1Str = $"{quantityPart} >= { startValue} and {quantityPart} <= {endValue} ";
                                if (Part1Check) part1FractionStr = $"{quantityPart} >= { startValue} and {quantityPart} <= {endValue} ";
                                if (All1Check) all1FractionStr = $"{quantityAll} >= { startValue} and {quantityAll} <= {endValue} ";
                            }
                            if (valueSelect1 == "manufacture_date" || valueSelect1 == "order_date")
                            {
                                part1Str = $"{valueSelect1} >= '{startDate}' and {valueSelect1} <= '{endDate}' ";
                                if (Part1Check) part1FractionStr = $"{valueSelect1} >= '{startDate}' and {valueSelect1} <= '{endDate}' ";
                                if (All1Check) all1FractionStr = $"{valueSelect1} >= '{startDate}' and {valueSelect1} <= '{endDate}' ";
                            }
                            break;
                    }
                    break;
            }

            if (isDefected) defected = "1"; else defected = "0";
            if (valueSelect1 == "defected")
            {
                part1Str = $"isdefected = {defected} ";
                if (Part1Check) part1FractionStr = $"isdefected = {defected} ";
                if (All1Check) all1FractionStr = $"isdefected = {defected} ";
            }

            wherePartStr = GetWhereStr(part1Str, part2Str, partAreaStr);
            wherePartFractionStr = GetWhereStr(part1FractionStr, part2FractionStr, partAreaStr);
            whereAllFractionStr = GetWhereStr(all1FractionStr, all2FractionStr, allAreaStr); 
            #endregion


            if(paramString1 == "Среднее")
            {
                CmdSearch = $"Select avg({valueSelect1}) as Среднее_значение from model {innerMainStr} {wherePartStr};";
                CmdPartFraction = "";
                CmdAllFraction = "";
                CmdQuantity = "";
            }
            else 
            {
                CmdSearch = $"Select distinct model.model_id, model_name, type_FK, weight, price, stock_balance, reserved, saled, manufacturer_FK, supplier_FK from model " +
                            $"{innerMainStr} {wherePartStr};";
                CmdPartFraction = $"select count(device_id) as PartFraction from model {innerPartFractionStr} {wherePartFractionStr};";
                CmdAllFraction = $"select count(device_id) as AllFraction from model {innerAllFractionStr} {whereAllFractionStr};";
                CmdQuantity = $"select count(device_id) as Quantity from model {innerQuantityStr} {wherePartStr};";
            }            

            str = await dataBaseService.SearchModelTableAsync(CmdSearch, CmdPartFraction, CmdAllFraction, CmdQuantity);
            return str;
        }

        public string GetFractionWhereStr(bool checkA, string valueA, string parameterA, string separator1, bool checkB, string valueB, string parameterB, string separator2 = null, bool checkC = false, string valueC = null, string parameterC = null)
        {
            string outString = "";

            if (separator2 != null)
            {
                if (separator1 == "or")
                {
                    if (separator2 == "or")
                    {
                        if (checkA && checkB && checkC)
                        {
                            outString = $"({valueA}_name = '{parameterA}' or {valueB}_name = '{parameterB}' or {valueC}_name = '{parameterC}') ";
                        }
                        else if (checkA && checkB)
                        {
                            outString = $"({valueA}_name = '{parameterA}' or {valueB}_name = '{parameterB}') ";
                        }
                        else if (checkA && checkC)
                        {
                            outString = $"({valueA}_name = '{parameterA}' or {valueC}_name = '{parameterC}') ";
                        }
                        else if (checkB && checkC)
                        {
                            outString = $"({valueB}_name = '{parameterB}' or {valueC}_name = '{parameterC}') ";
                        }
                        else
                        {
                            if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                            else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                            else if (checkC) outString = $"{valueC}_name = '{parameterC}' ";
                        }
                    }
                    else //and
                    {
                        if (checkA && checkB && checkC)
                        {
                            outString = $"({valueA}_name = '{parameterA}' or {valueB}_name = '{parameterB}') and {valueC}_name = '{parameterC}' ";
                        }
                        else if (checkA && checkB)
                        {
                            outString = $"({valueA}_name = '{parameterA}' or {valueB}_name = '{parameterB}') ";
                        }
                        else if (checkA && checkC)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueC}_name = '{parameterC}' ";
                        }
                        else if (checkB && checkC)
                        {
                            outString = $"{valueB}_name = '{parameterB}' and {valueC}_name = '{parameterC}' ";
                        }
                        else
                        {
                            if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                            else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                            else if (checkC) outString = $"{valueC}_name = '{parameterC}' ";
                        }
                    }
                }
                else //and
                {
                    if (separator2 == "or")
                    {
                        if (checkA && checkB && checkC)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and ({valueB}_name = '{parameterB}' or {valueC}_name = '{parameterC}') ";
                        }
                        else if (checkA && checkB)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueB}_name = '{parameterB}' ";
                        }
                        else if (checkA && checkC)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueC}_name = '{parameterC}' ";
                        }
                        else if (checkB && checkC)
                        {
                            outString = $"({valueB}_name = '{parameterB}' or {valueC}_name = '{parameterC}') ";
                        }
                        else
                        {
                            if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                            else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                            else if (checkC) outString = $"{valueC}_name = '{parameterC}' ";
                        }
                    }
                    else //and
                    {
                        if (checkA && checkB && checkC)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueB}_name = '{parameterB}' and {valueC}_name = '{parameterC}' ";
                        }
                        else if (checkA && checkB)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueB}_name = '{parameterB}' ";
                        }
                        else if (checkA && checkC)
                        {
                            outString = $"{valueA}_name = '{parameterA}' and {valueC}_name = '{parameterC}' ";
                        }
                        else if (checkB && checkC)
                        {
                            outString = $"{valueB}_name = '{parameterB}' and {valueC}_name = '{parameterC}' ";
                        }
                        else
                        {
                            if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                            else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                            else if (checkC) outString = $"{valueC}_name = '{parameterC}' ";
                        }
                    }
                }
            }
            else //==null
            {
                if (separator1 == "or")
                {
                    if (checkA && checkB)
                    {
                        outString = $"({valueA}_name = '{parameterA}' or {valueB}_name = '{parameterB}') ";
                    }
                    else
                    {
                        if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                        else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                    }
                }
                else //and
                {
                    if (checkA && checkB)
                    {
                        outString = $"{valueA}_name = '{parameterA}' and {valueB}_name = '{parameterB}' ";
                    }
                    else
                    {
                        if (checkA) outString = $"{valueA}_name = '{parameterA}' ";
                        else if (checkB) outString = $"{valueB}_name = '{parameterB}' ";
                    }
                }
            }
            return outString;
        }

        public string GetInnerStr(string parameterPartArea, string parameterAllArea, string valueA, string parameterA, string valueB, string parameterB, string valueC, string parameterC, bool checkA = true, bool checkB = true, bool checkC = true)
        {
            string outString = "";
            if (parameterPartArea != "везде" || parameterAllArea != "всего" ||
                (checkA && parameterA != "-" && (valueA == "manufacture_date" || valueA == "defected" || valueA == "order_date")) ||
                (checkB && parameterB != "-" && (valueB == "manufacture_date" || valueB == "defected" || valueB == "order_date")) ||
                (checkC && parameterC != "-" && (valueC == "manufacture_date" || valueC == "defected" || valueC == "order_date")))
            {
                outString += $"inner join device on model_FK = model.model_id ";
            }

            if ((checkA && parameterA != "-" && valueA == "order_date") ||
                (checkB && parameterB != "-" && valueB == "order_date") ||
                (checkC && parameterC != "-" && valueC == "order_date"))
            {
                outString += $"inner join [order] on order_FK = [order].order_id ";
            }

            if ((checkA && parameterA != "-" && valueA == "type" && parameterA != "-") ||
                (checkB && parameterB != "-" && valueB == "type" && parameterB != "-") ||
                (checkC && parameterC != "-" && valueC == "type" && parameterC != "-"))
            {
                outString += $"inner join [type] on type_FK = type_id ";
            }

            if ((checkA && parameterA != "-" && (valueA == "manufacturer" || valueA == "country") && parameterA != "-") ||
                (checkB && parameterB != "-" && (valueB == "manufacturer" || valueB == "country") && parameterB != "-") ||
                (checkC && parameterC != "-" && (valueC == "manufacturer" || valueC == "country") && parameterC != "-"))
            {
                outString += $"inner join manufacturer on manufacturer_FK = manufacturer_id ";
            }

            if ((checkA && parameterA != "-" && valueA == "country" && parameterA != "-") ||
                (checkB && parameterB != "-" && valueB == "country" && parameterB != "-") ||
                (checkC && parameterC != "-" && valueC == "country" && parameterC != "-"))
            {
                outString += $"inner join country on country_FK = country_id ";
            }

            if ((checkA && parameterA != "-" && valueA == "supplier" && parameterA != "-") ||
                (checkB && parameterB != "-" && valueB == "supplier" && parameterB != "-") ||
                (checkC && parameterC != "-" && valueC == "supplier" && parameterC != "-"))
            {
                outString += $"inner join supplier on supplier_FK = supplier_id ";
            }
            return outString;
        }

        public string GetWhereStr(string part1Str, string part2Str, string partAreaStr)
        {
            string whereStr = "";
            if (part1Str != "")
            {
                if (part1Str.Contains("select") == false)
                {
                    if (part2Str != "")
                    {
                        if (partAreaStr != "")
                        {
                            whereStr = $"where {part1Str} and {part2Str} and {partAreaStr}";
                        }
                        else whereStr = $"where {part1Str} and {part2Str}";
                    }
                    else
                    {
                        if (partAreaStr != "")
                        {
                            whereStr = $"where {part1Str} and {partAreaStr}";
                        }
                        else whereStr = $"where {part1Str}";
                    }
                }
                else
                {
                    if (part2Str != "")
                    {
                        if (partAreaStr != "")
                        {
                            whereStr = $"where {part1Str} where {part2Str} and {partAreaStr})  and {part2Str} and {partAreaStr}";
                        }
                        else whereStr = $"where {part1Str} where {part2Str}) and {part2Str}";
                    }
                    else
                    {
                        if (partAreaStr != "")
                        {
                            whereStr = $"where {part1Str} where {partAreaStr}) and {partAreaStr}";
                        }
                        else whereStr = $"where {part1Str})";
                    }
                }

            }
            else
            {
                if (part2Str != "")
                {
                    if (partAreaStr != "")
                    {
                        whereStr = $"where {part2Str} and {partAreaStr}";
                    }
                    else whereStr = $"where {part2Str}";
                }
                else
                {
                    if (partAreaStr != "") whereStr = $"where {partAreaStr}";
                }
            }
            return whereStr;
        }

    }

   
}
