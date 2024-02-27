export enum MetricInterval {
    Hour = 'Hourly',
    Day = 'Daily',
    Week = 'Weekly',
    Month = 'Monthly',
    Year = 'Yearly'
}




export const NumericToMetricIntervalMapping: { [key: number]: MetricInterval } = {
    0: MetricInterval.Hour,
    1: MetricInterval.Day,
    2: MetricInterval.Week,
    3: MetricInterval.Month,
    4: MetricInterval.Year
};