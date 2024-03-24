export interface Metric {
    id: number;
    value: number | null;
    valueString: string | null;
    created: string;
    status: number;
    metaJson: string | null;
    hashSHA256: string | null;
}