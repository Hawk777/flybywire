using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

// Thanks to James O’Hare for the 3D catenary calculation code at
// <https://gist.github.com/Farfarer/a765cd07920d48a8713a0c1924db6d70>. I
// modified that work to change the calculation from 3D to 2D and extract it
// into a stateless calculation function.
public class Catenary {
	public static Vector2[] generate(Vector2 start, Vector2 end, float length, int steps) {
		float lineDist = Vector2.Distance(end, start);
		float slack = length - lineDist;
		float lineDistH = Mathf.Abs(end.x - start.x);
		float l = lineDist + Mathf.Max(0.0001f, slack);
		float r = 0.0f;
		float s = start.y;
		float u = lineDistH;
		float v = end.y;

		if((u - r) == 0.0f) {
			return new Vector2[]{Vector2.zero, Vector2.zero};
		}

		float ztarget = Mathf.Sqrt(l * l - (v - s) * (v - s)) / (u - r);

		int loops = 30;
		int iterationCount = 0;
		int maxIterations = loops * 10; // For safety.
		bool found = false;

		float z = 0.0f;
		float ztest = 0.0f;
		float zstep = 100.0f;
		float ztesttarget = 0.0f;
		for (int i = 0; i < loops; i++) {
			for (int j = 0; j < 10; j++) {
				iterationCount++;
				ztest = z + zstep;
				ztesttarget = (float) Math.Sinh(ztest) / ztest;

				if (float.IsInfinity(ztesttarget))
					continue;

				if(ztesttarget == ztarget) {
					found = true;
					z = ztest;
					break;
				} else if(ztesttarget > ztarget) {
					break;
				} else {
					z = ztest;
				}

				if (iterationCount > maxIterations) {
					found = true;
					break;
				}
			}

			if(found) {
				break;
			}

			zstep *= 0.1f;
		}

		float a = (u - r) / 2.0f / z;
		float p = (r + u - a * Mathf.Log((l + v - s) / (l - v + s))) / 2.0f;
		float q = (v + s - l * (float)Math.Cosh(z) / (float) Math.Sinh(z)) / 2.0f;

		Vector2[] points = new Vector2[steps];
		float stepsf = steps - 1;
		float stepf;
		for(int i = 0; i < steps; i++) {
			stepf = i / stepsf;
			Vector2 pos = Vector2.zero;
			pos.x = Mathf.Lerp(start.x, end.x, stepf);
			pos.y = a * (float) Math.Cosh(((stepf * lineDistH) - p) / a) + q;
			points[i] = pos;
		}

		return points;
	}
}
