const RenderJsonData: React.FC<{ data: []; }> = ({ data }) => {
    if (typeof data === 'object' && data !== null) {
      return (
        <div>
          {Object.entries(data).map(([key, value]) => (
            <div key={key}>
              <strong>{key}:</strong> <RenderJsonData data={value} />
            </div>
          ))}
        </div>
      );
    } else {
      return <span>{data}</span>;
    }
  };

export function RenderMetaJson(metaJson: string): React.ReactNode {
    const data = JSON.parse(metaJson);
    return <RenderJsonData data={data} />;
  }