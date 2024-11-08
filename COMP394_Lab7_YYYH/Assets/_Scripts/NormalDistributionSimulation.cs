using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalDistributionSimulation : MonoBehaviour
{
    public int binCount = 20;
    public float mu = 0;
    public float sigma = 1;
    public int sampleCount = 500;

    public GameObject barPrefab;
    public Transform histogramContainer;

    private List<float> dataPoints;

    void Start()
    {
        dataPoints = new List<float>();
        for (int i = 0; i < sampleCount; i++)
        {
            dataPoints.Add(Normal(mu, sigma, 8));
        }

        DrawHistogram(dataPoints, binCount);
    }

    float Normal01(int count)
    {
        float sum = 0;
        for (int i = 0; i < count; i++)
        {
            sum += UnityEngine.Random.value;
        }
        return (sum - count / 2.0f) / Mathf.Sqrt(count / 12.0f);
    }

    float Normal(float mu, float sigma, int count)
    {
        return mu + sigma * Normal01(count);
    }

    void DrawHistogram(List<float> points, int bins)
    {
        float minValue = Mathf.Min(points.ToArray());
        float maxValue = Mathf.Max(points.ToArray());

        float binWidth = (maxValue - minValue) / bins;
        int[] histogram = new int[bins];

        foreach (var point in points)
        {
            int binIndex = Mathf.Clamp((int)((point - minValue) / binWidth), 0, bins - 1);
            histogram[binIndex]++;
        }

        VisualizeHistogram(histogram);
    }

    void VisualizeHistogram(int[] histogram)
    {
        for (int i = 0; i < histogram.Length; i++)
        {
            GameObject bar = Instantiate(barPrefab, histogramContainer);
            RectTransform rt = bar.GetComponent<RectTransform>();

            float height = histogram[i] * 10;
            rt.sizeDelta = new Vector2(rt.sizeDelta.x, height);

            rt.anchoredPosition = new Vector2(i * (rt.sizeDelta.x + 5), 0);
        }
    }
}
